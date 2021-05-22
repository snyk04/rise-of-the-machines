using System.Collections;
using System.Collections.Generic;
using Project.Classes.Damagable;
using Project.Scripts.Characters;
using Project.Scripts.CharactersAndObjects;
using UnityEngine;

namespace Project.Scripts.Objects
{
    public class SmartGunController : GunController
    {
        [Header("Rotating timings")]
        [SerializeField] private float rotateTime;
        
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

        public void ShootABurst()
        {
            if (Weapon.WeaponData.isReloading)
            {
                return;
            }
            
            StartCoroutine(ShootingABurst());
        }
        private IEnumerator ShootingABurst()
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
                var lookAtEnemyCoroutine = StartCoroutine(RotateToEnemy(enemyPosition));
                yield return new WaitForSeconds(rotateTime * 3);
                for (int i = 0; i < Weapon.WeaponData.maxBulletsInMagazine; i++)
                {
                    if (collider == null)
                    {
                        break;
                    }
                    
                    TryShoot();
                    
                    yield return new WaitForSeconds(1 / weaponData.shotsPerSecond);
                }
                StopCoroutine(lookAtEnemyCoroutine);
            }
            var rotatingCoroutine = StartCoroutine(RotateToDefaultPosition());
            yield return new WaitForSeconds(rotateTime * 3);
            StopCoroutine(rotatingCoroutine);
            yield return null;
        }
        
        private IEnumerator RotateToEnemy(Vector3 enemyPosition)
        {
            var velocity = Vector3.zero;
            
            while (true)
            {
                var goalForward = Quaternion.Euler(0, 90, 0) * (enemyPosition - transform.position).normalized;
                transform.forward = Vector3.SmoothDamp(transform.forward, goalForward , ref velocity, rotateTime, 1000);
                yield return new WaitForEndOfFrame();
            }
        }
        private IEnumerator RotateToDefaultPosition()
        {
            var velocity = Vector3.zero;
            while (true)
            {
                var goalForward = Quaternion.Euler(0, 90, 0) * Player.Instance.Transform.forward;
                transform.forward = Vector3.SmoothDamp(transform.forward, goalForward , ref velocity, rotateTime, 1000);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
