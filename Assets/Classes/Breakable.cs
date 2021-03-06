using UnityEngine;

namespace Classes
{
    public class Breakable : IDamageable
    {
        private float health;
        public bool IsDead() => Health.Equals(0);
        public event IDamageable.OnHealthZero healthZero;

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
        public Transform Transform { get; }

        protected Breakable(float maxhealth, Transform transform)
        {
            MaxHealth = maxhealth;
            health = maxhealth;
            Transform = transform;
        }

        public void Die()
        {
            throw new System.NotImplementedException();
        }
        public void TakeDamage(float damage)
        {
            health -= damage;
        }
    }
}
