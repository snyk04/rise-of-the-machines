using CharactersAndObjects;
using Classes;
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
        private float timeAfterLastShot;
        private Coroutine animateShoot;

        private void Awake()
        {
            weapon = Weapon.CreateWeapon(weaponSO);
            gunSound = GetComponent<GunSound>();

            lineRenderer = lineRenderer ? lineRenderer : gameObject.AddComponent<LineRenderer>();
            ClearLineRenderer();
            lineRenderer.widthMultiplier = 0.1f;
        }
        private void Update()
        {
            timeAfterLastShot += Time.deltaTime;
        }

        private List<Ray> rays = new List<Ray>();
        private List<Vector3> positions = new List<Vector3>();

        public void TryShoot()
        {
            Weapon.ShotResult shotResult = weapon.TryShoot(timeAfterLastShot);

            switch (shotResult)
            {
                case Weapon.ShotResult.NoAmmoInBackpack:
                    NoAmmo();
                    break;
                case Weapon.ShotResult.NoAmmoInMagazine:
                    Reload();
                    break;
                case Weapon.ShotResult.ShotSuccesful:
                    Shoot();
                    break;
                case Weapon.ShotResult.TooFast:
                    return;
            }
        }

        private void Shoot()
        {
            timeAfterLastShot = 0;

            rays.Clear();
            positions.Clear();
            WeaponSO weaponData = weapon.WeaponData;

            var muzzleHolePos = muzzleHole.position;

            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                var localShootDir = muzzleHole.rotation * SimpsonsSpreading.Spreading(weaponData.shotSpread);
                localShootDir.y = 0;
                rays.Add(new Ray(muzzleHolePos, localShootDir));
            }

            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                positions.Add(muzzleHolePos);
                positions.Add(rays[i].direction.normalized * weapon.WeaponData.maxShotDistance + muzzleHolePos);
            }

            if (animateShoot != null)
            {
                StopCoroutine(animateShoot);
            }
            animateShoot = StartCoroutine(VisualizeShot(positions.ToArray()));

            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                if (Physics.Raycast(rays[i], out RaycastHit hitInfo, weaponData.maxShotDistance))
                {
                    if (hitInfo.transform.TryGetComponent(out Damageable damageable))
                    {
                        var amountOfDamage = Random.Range(weaponData.damage * (1 - weaponData.damageSpread), weaponData.damage * (1 + weaponData.damageSpread)) / weaponData.bulletsPerShot;
                        damageable.TakeDamage(amountOfDamage);
                    }
                    if (hitInfo.transform.TryGetComponent(out ParticlesManager particlesManager))
                    {
                        particlesManager.EmitAllParticles(hitInfo.point);
                    }
                }
            }

            foreach (ParticleSystem particle in shotParticles)
            {
                particle.Emit(1);
            }
            gunSound.PlayShotSound();
        }
        private void Reload()
        {
            // TODO: заблокировать возможность стрелять
            timeAfterLastShot = 0;
            weapon.Reload();
            gunSound.PlayReloadSound();
        }
        private void NoAmmo()
        {
            timeAfterLastShot = 0;
            gunSound.PlayNoAmmoSound();
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
