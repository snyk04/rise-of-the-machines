using System.Collections;
using System.Collections.Generic;
using Characters;
using CharactersAndObjects;
using Classes;
using UnityEngine;

namespace Objects
{
    public class SmartGunController : GunController
    {
        private Transform enemyTransform;
        private Vector3 enemyPosition;

        private protected override void Shoot(List<Ray> rays)
        {
            if (enemyTransform.TryGetComponent(out DamageableController damageable))
            {
                var amountOfDamage = Random.Range(weaponData.damage * (1 - weaponData.damageSpread), weaponData.damage * (1 + weaponData.damageSpread));
                damageable.TakeDamage(amountOfDamage);
            }
            if (enemyTransform.TryGetComponent(out ParticlesManager particlesManager))
            {
                particlesManager.EmitAllParticles(enemyPosition, (Player.Instance.Transform.position - enemyPosition).normalized);
            }
            if (enemyTransform.TryGetComponent(out HitSoundManager hitSoundManager))
            {
                hitSoundManager.PlayRandomClip();
            }
            foreach (ParticleSystem particle in shotParticles)
            {
                particle.Emit(1);
            }
            gunSound.PlayShotSound();
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

        public void ShootSomehow()
        {
            if (Weapon.WeaponData.isReloading)
            {
                return;
            }
            
            StartCoroutine(ShootingSomehow());
        }
        private IEnumerator ShootingSomehow()
        {
            var colliders = Physics.OverlapSphere(Player.Instance.Transform.position, weaponData.maxShotDistance);
            foreach (Collider collider in colliders)
            {
                if (!collider.transform.TryGetComponent(out EnemyController enemyController))
                {
                    continue;
                }

                enemyTransform = collider.transform;
                enemyPosition = enemyTransform.position;
                var rotatingCoroutine = StartCoroutine(LookAtEnemy(enemyPosition, 0, 90, 0));
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < Weapon.WeaponData.maxBulletsInMagazine; i++)
                {
                    if (collider == null)
                    {
                        break;
                    }
                    
                    TryShoot();
                    
                    yield return new WaitForSeconds(1 / weaponData.shotsPerSecond);
                }
                StopCoroutine(rotatingCoroutine);
            }
            yield return null;
        }
    }
}
