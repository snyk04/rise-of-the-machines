using UnityEngine;

namespace Classes
{
    public class Breakable : IDamageable
    {
        private float health;
        public bool IsDead() => Health.Equals(0);
        public delegate void OnHealthZero();
        public event OnHealthZero healthZero;

        public float Health 
        {
            get => health;
            private set
            {
                health = Mathf.Clamp(value, 0, MaxHealth);
                if (health <= 0)
                    healthZero?.Invoke();
            }
        }
        public float MaxHealth { get; }
        public Transform Transform { get; }

        public Breakable(float maxhealth, Transform transform)
        {
            MaxHealth = maxhealth;
            health = maxhealth;
            Transform = transform;
            healthZero += Die;
        }

        public void Die()
        {
            //TODO: animation
            Object.Destroy(Transform.gameObject);
        }
        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
    }
}
