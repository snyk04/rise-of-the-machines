using UnityEngine;

namespace Classes
{
    public abstract class Person : IDamageable
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
                if (health.Equals(0))
                    healthZero?.Invoke();
            }
        }
        public float MaxHealth { get; }
        public float MoveSpeed { get; }
        public Transform Transform { get; }

        protected Person(float maxHealth, float moveSpeed, Transform transform)
        {
            MaxHealth = maxHealth;
            health = maxHealth;
            MoveSpeed = moveSpeed;
            Transform = transform;
            healthZero += Die;
        }

        public virtual void Die() { }
        public virtual void Attack() { }

        public void TakeDamage(float damage)
        {
            Health -= damage;
        }
        public void RestoreHealth(float heal)
        {
            Health += heal;
        }
    }
}
