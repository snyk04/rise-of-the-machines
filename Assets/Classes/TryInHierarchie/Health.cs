using UnityEngine;

namespace Classes.TryInHierarchie {
    public class Health : Characteristic {
        public delegate void HealthZero();

        private event HealthZero HealthZeroEvent;

        private float maxHP;
        public float MaxHP {
            get => maxHP;
            private set {
                var percentHP = HP / maxHP;
                maxHP = value > 0 ? value : 0;
                HP = maxHP * percentHP;
            }
        }

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