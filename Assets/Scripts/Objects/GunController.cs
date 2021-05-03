using CharactersAndObjects;
using Classes.ScriptableObjects;
using Classes.TryInHierarchie;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;

namespace Objects
{
    public class GunController : MonoBehaviour
    {
        [Header("Gun prefab settings")]
        [SerializeField] private WeaponSO weaponSO;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform muzzleHole;
        [SerializeField] private ParticleSystem[] shotParticles;

        public Weapon weapon { get; private set; }
        private GunSound gunSound;
        private Coroutine animateShoot;

        private void Awake()
        {
            weapon = Weapon.CreateWeapon(Instantiate(weaponSO));
            gunSound = GetComponent<GunSound>();

            lineRenderer = lineRenderer ? lineRenderer : gameObject.AddComponent<LineRenderer>();
            ClearLineRenderer();
            lineRenderer.widthMultiplier = 0.1f;

            weapon.OnReloadStart += gunSound.PlayReloadSound;
        }

        private List<Ray> rays = new List<Ray>();

        public void TryShoot()
        {
            Weapon.ShotResult shotResult = weapon.TryShoot(Time.time, muzzleHole.position, muzzleHole.rotation, out rays);

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

        private void Shoot(List<Ray> rays)
        {
            positions.Clear();
            WeaponSO weaponData = weapon.WeaponData;

            var muzzleHolePos = muzzleHole.position;

            VisualizeShot(weaponData, muzzleHolePos);

            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                if (Physics.Raycast(rays[i], out RaycastHit hitInfo, weaponData.maxShotDistance))
                {
                    if (hitInfo.transform.TryGetComponent(out DamageableController damageable))
                    {
                        var amountOfDamage = Random.Range(weaponData.damage * (1 - weaponData.damageSpread), weaponData.damage * (1 + weaponData.damageSpread)) / weaponData.bulletsPerShot;
                        damageable.TakeDamage(amountOfDamage);
                    }
                    if (hitInfo.transform.TryGetComponent(out ParticlesManager particlesManager))
                    {
                        particlesManager.EmitAllParticles(hitInfo);
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
            if (weapon.WeaponData.isReloading)
            {
                return;
            }

            StartCoroutine(weapon.Reload());
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
    }
}
