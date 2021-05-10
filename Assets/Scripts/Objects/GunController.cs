using CharactersAndObjects;
using Classes.ScriptableObjects;
using Classes.TryInHierarchie;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections;
using System.Collections.Generic;
using Characters;
using Classes;

namespace Objects
{
    public class GunController : MonoBehaviour
    {
        # region Properties
        
        [Header("Gun prefab settings")]
        [SerializeField] private WeaponSO weaponSo;
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform muzzleHole;
        [SerializeField] private ParticleSystem[] shotParticles;

        public Weapon Weapon { get; private set; }
        private GunSound gunSound;
        private Coroutine animateShoot;
        private WeaponSO weaponData;
        
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
                    if (weaponData.isSmart)
                    {
                        ShootSmartly();
                    }
                    else
                    {
                        ShootManually(rays);
                    }
                    break;
                case Weapon.ShotResult.TooFast:
                    return;
                default:
                    return;
            }
        }

        private List<Vector3> positions = new List<Vector3>();
        private void ShootManually(List<Ray> rays)
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

        private void ShootSmartly()
        {
            StartCoroutine(ShootingSmartly());
        }
        private IEnumerator ShootingSmartly()
        {
            var colliders = Physics.OverlapSphere(Player.Instance.Transform.position, weaponData.maxShotDistance);
            foreach (Collider collider in colliders)
            {
                if (!collider.transform.TryGetComponent(out EnemyController enemyController))
                {
                    continue;
                }
                
                var enemyPosition = collider.transform.position;
                var rotatingCoroutine = StartCoroutine(LookAtEnemy(enemyPosition, 0, 90, 0));
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < 10; i++)
                {
                    if (collider.transform.TryGetComponent(out DamageableController damageable))
                    {
                        var amountOfDamage = Random.Range(weaponData.damage * (1 - weaponData.damageSpread), weaponData.damage * (1 + weaponData.damageSpread));
                        damageable.TakeDamage(amountOfDamage);
                    }
                    if (collider.transform.TryGetComponent(out ParticlesManager particlesManager))
                    {
                        particlesManager.EmitAllParticles(enemyPosition, (Player.Instance.Transform.position - enemyPosition).normalized);
                    }
                    if (collider.transform.TryGetComponent(out HitSoundManager hitSoundManager))
                    {
                        hitSoundManager.PlayRandomClip();
                    }
                
                    foreach (ParticleSystem particle in shotParticles)
                    {
                        particle.Emit(1);
                    }
                    gunSound.PlayShotSound();

                    yield return new WaitForSeconds(1 / weaponData.shotsPerSecond);
                }
                

                StopCoroutine(rotatingCoroutine);

                yield break;
            }
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
        
        private IEnumerator LookAtEnemy(Vector3 enemyPosition, float xRotation, float yRotation, float zRotation)
        {
            var velocity = Vector3.zero;
            while (true)
            {
                var enemyPlayerVector = (enemyPosition - transform.position).normalized;
                var rotation = Quaternion.Euler(xRotation, yRotation, zRotation);
                var rotatedVector = rotation * enemyPlayerVector;
                transform.forward = Vector3.SmoothDamp(transform.forward, rotatedVector, ref velocity, 0.1f, 1000);
                yield return new WaitForEndOfFrame();
            }
        }

        #endregion
    }
}
