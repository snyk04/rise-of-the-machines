using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Classes.ScriptableObjects;
using Classes.TryInHierarchie;

namespace Objects
{
    public class GunController : MonoBehaviour
    {
        [Header("Some shit")]
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
        [SerializeField] private float averageDamage;
        [SerializeField] private int amountOfBullets;
        [Range(0, 1)] [SerializeField] private float horizontalScatter;

        [Header("General shooting settings")]
        [SerializeField] private float maxHorizontalScatterAngle;

        private Weapon weapon;

        private void Start()
        {
            for (int i = 0; i < amountOfBullets; i++)
            {
                GameObject hitMarkerObject = Instantiate(hitMarker, Vector3.zero, Quaternion.identity, hitMarkerArray);
                hitMarkerObject.SetActive(false);
            }

            weapon = Weapon.CreateWeapon(weaponSO);
        }

        public void Shoot()
        {
            shotParticle_1.Emit(1);
            shotParticle_2.Emit(1);
            shotParticle_3.Emit(1);

            var rays = new List<Ray>();
            for (int i = 0; i < amountOfBullets; i++)
            {
                var localShootDir = muzzleHole.rotation * Spreading(1);
                localShootDir.y = 0;
                var shootDirection = muzzleHole.position + localShootDir;
                
                lineRenderer.SetPositions(new []{ muzzleHole.position, shootDirection});
                rays.Add(new Ray(muzzleHole.position, shootDirection));
            }

            StartCoroutine(ShootSoundManager());

            for (int i = 0; i < amountOfBullets; i++)
            {
                if (Physics.Raycast(rays[i], out RaycastHit hitInfo, 25))
                {
                    if (hitInfo.transform.TryGetComponent(out Damageable damageable))
                    {
                        float amountOfDamage = Random.Range(averageDamage * 0.9f, averageDamage * 1.1f) / amountOfBullets;
                        damageable.TakeDamage(amountOfDamage);
                        hitMarkerArray.GetChild(i).position = hitInfo.point;
                        hitMarkerArray.GetChild(i).gameObject.SetActive(true);
                    }
                }
            }
            
        }
        private IEnumerator ShootSoundManager()
        {
            AudioSource source = shootPoint.AddComponent<AudioSource>();

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
            return Mathf.Pow(random / 0.5f, 0.5f) * a - a;
        }
        private float FindRandNumberUsingSimpson(float random, float a)
        {
            return random <= 0.5 ? FindG(random, a) : -FindG(1 - random, a);
        }
        private Vector3 Spreading(float a) {
            return new Vector3(FindRandNumberUsingSimpson(Random.value, a), 0, 100).normalized;
        }
    }
}
