using UnityEngine;

namespace Classes {
    public abstract class Person {
        private float health;
        public bool IsDead => Health.Equals(0);

        public delegate void OnHealthZero();

        public event OnHealthZero healthZero;

        protected Person(float health, float maxHealth, float moveSpeed) {
            this.health = health;
            MaxHealth = maxHealth;
            MoveSpeed = moveSpeed;
            healthZero += Die;
        }

        public float Health {
            get => health;
            private set {
                health = Mathf.Clamp(value, 0, MaxHealth);
                if (health == 0)
                    healthZero?.Invoke();
            }
        }

        public float MaxHealth { get; }

        public float MoveSpeed { get; }

        public virtual void Die() { }

        public void TakeDamage(float damage) {
            Health -= damage;
        }

        public void RestoreHealth(float heal) {
            Health += heal;
        }
    }
}