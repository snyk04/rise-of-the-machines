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
        private float timeAfterLastShot;
        private Coroutine animateShoot;

        private void Awake()
        {
            weapon = Weapon.CreateWeapon(weaponSO);

            lineRenderer = lineRenderer ? lineRenderer : gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            ClearLineRenderer();
            lineRenderer.widthMultiplier = 0.1f;
        }
        private void Update()
        {
            timeAfterLastShot += Time.deltaTime;
        }

        public void Shoot()
        {
            WeaponSO weaponData = weapon.WeaponData;

            if (timeAfterLastShot < 1 / weaponData.shotsPerSecond)
            {
                return;
            }
            timeAfterLastShot = 0;

            var rays = new List<Ray>();
            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                var localShootDir = muzzleHole.rotation * SimpsonsSpreading.Spreading(weaponData.shotSpread);
                localShootDir.y = 0;
                var muzzleHolePos = muzzleHole.position;
                if (animateShoot != null)
                {
                    StopCoroutine(animateShoot);
                }
                animateShoot = StartCoroutine(VisualizeShot(muzzleHolePos, localShootDir));

                rays.Add(new Ray(muzzleHolePos, localShootDir));
            }

            for (int i = 0; i < weaponData.bulletsPerShot; i++)
            {
                if (Physics.Raycast(rays[i], out RaycastHit hitInfo, weaponData.maxShotDistance))
                {
                    if (hitInfo.transform.TryGetComponent(out Damageable damageable))
                    {
                        var amountOfDamage = Random.Range(weaponData.damage * (1 - weaponData.damageSpread), weaponData.damage * (1 + weaponData.damageSpread)) / weaponData.bulletsPerShot; // todo remove magic numbers, replace many same invokes to fields
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
            StartCoroutine(PlayShotSound());
        }

        private IEnumerator PlayShotSound()
        {
            AudioSource source = gameObject.AddComponent<AudioSource>(); // todo pull objects? 

            source.clip = weaponSO.shootSound;
            source.minDistance = 1;
            source.maxDistance = 50;
            source.volume = 1f;
            source.spatialBlend = 1f;
            source.Play();

            yield return new WaitForSeconds(source.clip.length);
            Destroy(source);
        }

        private IEnumerator VisualizeShot(Vector3 muzzleHolePos, Vector3 localShootDirection)
        {
            lineRenderer.SetPositions(new []{ muzzleHolePos, localShootDirection.normalized * weapon.WeaponData.maxShotDistance + muzzleHolePos});
            yield return new WaitForSeconds(0.1f);
            ClearLineRenderer();
        }

        private void ClearLineRenderer()
        {
            for (int i = 0; i < lineRenderer.positionCount; i++)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
            }
        }
    }
}
