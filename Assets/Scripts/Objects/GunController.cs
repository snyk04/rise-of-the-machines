using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Classes.ScriptableObjects;
using Classes.TryInHierarchie;
using Random = UnityEngine.Random;

namespace Objects
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private Transform muzzleHole;
        [SerializeField] private LayerMask damageableLayer;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private GameObject shootPoint;
        [SerializeField] private ParticleSystem shotParticle_1;
        [SerializeField] private ParticleSystem shotParticle_2;
        [SerializeField] private ParticleSystem shotParticle_3;
        [SerializeField] private GameObject hitMarker;
        [SerializeField] private Transform hitMarkerArray;
        [SerializeField] private LineRenderer lineRenderer;

        [Header("Gun prefab settings")] 
        [SerializeField] private WeaponSO weaponSO;

        public Weapon weapon { get; private set; }
        private float timeAfterLastShot;
        private Coroutine animateShoot;

        private void Awake() {
            weapon = Weapon.CreateWeapon(weaponSO);

            lineRenderer = lineRenderer ? lineRenderer : gameObject.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            ClearLineRenderer();
            lineRenderer.widthMultiplier = 0.1f;
        }

        private void Start()
        {
            for (int i = 0; i < weapon.WeaponData.amountOfBullets; i++)
            {
                GameObject hitMarkerObject = Instantiate(hitMarker, Vector3.zero, Quaternion.identity, hitMarkerArray);
                hitMarkerObject.SetActive(false);
            }
        }

        private void Update() {
            timeAfterLastShot += Time.deltaTime;
        }

        private IEnumerator AnimateShoot(Vector3 muzzleHolePos, Vector3 shootDirection) {
            lineRenderer.SetPositions(new []{ muzzleHolePos, (shootDirection - muzzleHolePos).normalized * weapon.WeaponData.maxDistance});
            yield return new WaitForSeconds(0.1f);
            ClearLineRenderer();
        }

        private void ClearLineRenderer() {
            for (int i = 0; i < lineRenderer.positionCount; i++) {
                lineRenderer.SetPosition(i, Vector3.zero);
            }
        }

        public void Shoot()
        {
            if (timeAfterLastShot < 1 / weapon.WeaponData.fireRate) {
                return;
            }
            timeAfterLastShot = 0;

            shotParticle_1.Emit(1);
            shotParticle_2.Emit(1);
            shotParticle_3.Emit(1);

            var rays = new List<Ray>();
            for (int i = 0; i < weapon.WeaponData.amountOfBullets; i++)
            {
                var localShootDir = muzzleHole.rotation * Spreading(weapon.WeaponData.spread);
                localShootDir.y = 0;
                var muzzleHolePos = muzzleHole.position;
                var shootDirection = muzzleHolePos + localShootDir;
                if (animateShoot != null) {
                    StopCoroutine(animateShoot);
                }
                animateShoot = StartCoroutine(AnimateShoot(muzzleHolePos, shootDirection));

                rays.Add(new Ray(muzzleHolePos, shootDirection - muzzleHolePos));
            }

            StartCoroutine(ShootSoundManager());

            for (int i = 0; i < weapon.WeaponData.amountOfBullets; i++)
            {
                if (Physics.Raycast(rays[i], out RaycastHit hitInfo, weapon.WeaponData.maxDistance))
                {
                    if (hitInfo.transform.TryGetComponent(out Damageable damageable))
                    {
                        var amountOfDamage = Random.Range(weapon.WeaponData.damage * 0.9f, weapon.WeaponData.damage * 1.1f) / weapon.WeaponData.amountOfBullets; // todo remove magic numbers, replace many same invokes to fields
                        damageable.TakeDamage(amountOfDamage);
                        hitMarkerArray.GetChild(i).position = hitInfo.point; // todo looks VERY bad, just use list of gameObjects
                        hitMarkerArray.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
            
        }
        private IEnumerator ShootSoundManager()
        {
            AudioSource source = shootPoint.AddComponent<AudioSource>(); // todo pull objects? 

            source.clip = shootSound;
            source.minDistance = 1;
            source.maxDistance = 50;
            source.volume = 1f;
            source.spatialBlend = 1f;
            source.Play();

            yield return new WaitForSeconds(source.clip.length);
            Destroy(source);
        }

        private float FindG(float random, float a)
        {
            return Mathf.Sqrt(2 * random) * a - a;
        }
        private float FindRandNumberUsingSimpson(float random, float a)
        {
            return random <= 0.5 ? FindG(random, a) : -FindG(1 - random, a);
        }
        private Vector3 Spreading(float a) {
            return new Vector3(FindRandNumberUsingSimpson(Random.value, a), 0, WeaponSO.DEFAULT_SPREAD_DISTANCE).normalized;
        }
    }
}
