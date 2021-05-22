using System.Collections;
using System.Collections.Generic;
using Project.Classes.ItemsAndInventory.Equipment;
using Project.Classes.ScriptableObjects;
using Project.Scripts.CharactersAndObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Objects
{
    public class GunController : MonoBehaviour
    {
        # region Properties
        
        [Header("Gun prefab settings")]
        [SerializeField] private WeaponSO weaponSo;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform muzzleHole;
        [SerializeField] private protected ParticleSystem[] shotParticles;

        public Weapon Weapon { get; private set; }
        private protected GunSound gunSound;
        private Coroutine animateShoot;
        private protected WeaponSO weaponData;
        
        #endregion

        #region Behaviour methods

        private void Awake()
        {
            Weapon = Weapon.CreateWeapon(Instantiate(weaponSo));
            gunSound = GetComponent<GunSound>();
            weaponData = Weapon.WeaponData;

            lineRenderer = lineRenderer ? lineRenderer : gameObject.AddComponent<LineRenderer>();
            ClearLineRenderer();
            lineRenderer.widthMultiplier = 0.1f;

            Weapon.OnReloadStart += gunSound.PlayReloadSound;
        }

        #endregion

        #region Methods

        private List<Ray> rays = new List<Ray>();
        public void TryShoot()
        {
            Weapon.ShotResult shotResult = Weapon.TryShoot(Time.time, muzzleHole.position, muzzleHole.rotation, out rays);

            switch (shotResult)
            {
                case Weapon.ShotResult.NoAmmoInBackpack:
                    NoAmmo();
                    break;
                case Weapon.ShotResult.NoAmmoInMagazine:
                    Reload();
                    break;
                case Weapon.ShotResult.ShotSuccessful:
                    Shoot(rays);
                    break;
                case Weapon.ShotResult.TooFast:
                    return;
                default:
                    return;
            }
        }

        private List<Vector3> positions = new List<Vector3>();

        private protected virtual void Shoot(List<Ray> rays)
        {
            positions.Clear();

            var muzzleHolePos = muzzleHole.position;

            VisualizeShot(weaponData, muzzleHolePos);

            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                if (Physics.Raycast(rays[i], out RaycastHit hitInfo, weaponData.maxShotDistance))
                {
                    if (hitInfo.transform.TryGetComponent(out DamageableController damageable))
                    {
                        var amountOfDamage = Random.Range(weaponData.damage * (1 - weaponData.damageSpread), weaponData.damage * (1 + weaponData.damageSpread));
                        damageable.TakeDamage(amountOfDamage);
                    }
                    if (hitInfo.transform.TryGetComponent(out ParticlesManager particlesManager))
                    {
                        particlesManager.EmitAllParticles(hitInfo.point, hitInfo.normal);
                    }
                    if (hitInfo.transform.TryGetComponent(out HitSoundManager hitSoundManager))
                    {
                        hitSoundManager.PlayRandomClip();
                    }
                }
            }

            foreach (ParticleSystem particle in shotParticles)
            {
                particle.Emit(1);
            }
            gunSound.PlayShotSound();
        }

        public void Reload()
        {
            if (Weapon.WeaponData.isReloading)
            {
                return;
            }

            StartCoroutine(Weapon.Reload());
        }
        private void NoAmmo()
        {
            gunSound.PlayNoAmmoSound();
        }

        private void VisualizeShot(WeaponSO weaponData, Vector3 muzzleHolePos)
        {
            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                positions.Add(muzzleHolePos);
                positions.Add(rays[i].direction.normalized * weaponData.maxShotDistance + muzzleHolePos);
            }

            if (animateShoot != null)
            {
                StopCoroutine(animateShoot);
            }
            animateShoot = StartCoroutine(VisualizeShot(positions.ToArray()));
        }

        private IEnumerator VisualizeShot(Vector3[] positions)
        {
            lineRenderer.positionCount = positions.Length;
            lineRenderer.SetPositions(positions);
            yield return new WaitForSeconds(0.1f);
            ClearLineRenderer();
        }

        private void ClearLineRenderer()
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.positionCount = 0;
            }
        }
        
        #endregion
    }
}
