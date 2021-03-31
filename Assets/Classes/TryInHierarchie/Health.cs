using UnityEngine;

namespace Classes.TryInHierarchie {
    public class Health : Characteristic, IDamageable {
        public delegate void HealthZero();

        private event HealthZero HealthZeroEvent;

        private float MaxHP { get; }
        
        public float HP {
            get => Value;
            private set {
                Value = Mathf.Clamp(value, 0, MaxHP);
                if (Value.Equals(0)) {
                    HealthZeroEvent?.Invoke();
                }
            }
        }

        public Health(float maxHp, HealthZero healthZeroMethod) : base(maxHp) {
            MaxHP = maxHp;
            HealthZeroEvent += healthZeroMethod;
        }

        public bool IsDead() => HP <= 0;


        public void TakeDamage(float damage) {
            HP -= damage;
        }

        public void RestoreHealth(float heal) {
            HP += heal;
        }

        public void HealToMaxHP() => HP = MaxHP;
    }
}