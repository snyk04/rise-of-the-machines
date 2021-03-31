using UnityEngine;
using System.Collections;

namespace Objects
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private Transform muzzleHole;
        [SerializeField] private LayerMask damageableLayer;
        [SerializeField] private float averageDamage;
        [SerializeField] private AudioClip shootSound;
        [SerializeField] private GameObject shootPoint;
        [SerializeField] private ParticleSystem shotParticle_1;
        [SerializeField] private ParticleSystem shotParticle_2;
        [SerializeField] private ParticleSystem shotParticle_3;

        private void OnDrawGizmos()
        {
            Debug.DrawRay(muzzleHole.position, muzzleHole.forward * 25);
        }

        public void Shoot()
        {
            shotParticle_1.Emit(1);
            shotParticle_2.Emit(1);
            shotParticle_3.Emit(1);
            var shootingRay = new Ray(muzzleHole.position, muzzleHole.forward);
            StartCoroutine(ShootSoundManager());
            if (Physics.Raycast(shootingRay, out RaycastHit hitInfo, 25))
            {
                if (hitInfo.transform.TryGetComponent(out Damageable damageable))
                {
                    float amountOfDamage = Random.Range(averageDamage * 0.9f, averageDamage * 1.1f);
                    damageable.TakeDamage(amountOfDamage);
                }
            }
        }
        IEnumerator ShootSoundManager()
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
    }
}
