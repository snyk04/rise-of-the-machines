using Project.Classes.Damagable;
using Project.Interfaces;
using UnityEngine;

namespace Project.Scripts.CharactersAndObjects
{
    public class DamageableController : MonoBehaviour
    {
        private float maxHealth;

        public IDamageable DamageableClass { get; private set; }

        public void Initialize(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator = null)
        {
            this.maxHealth = maxHealth;
            DamageableClass = new Enemy(maxHealth, armor, moveSpeed, transform, animator);
        }

        public void TakeDamage(float damage)
        {
            DamageableClass = DamageableClass ?? new Breakable(maxHealth, transform);
            DamageableClass.Health.TakeDamage(damage);
        }
    }
}
