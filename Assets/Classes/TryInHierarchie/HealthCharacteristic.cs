using UnityEngine;

namespace Classes.TryInHierarchie {
    public class HealthCharacteristic : Characteristic {
        public delegate void HealthZero();
        public delegate void HpChange();

        private event HealthZero HealthZeroEvent;
        public event HpChange OnHpChange;

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

        public HealthCharacteristic(float maxHp, HealthZero healthZeroMethod) : base(maxHp) {
            MaxHP = maxHp;
            HealthZeroEvent += healthZeroMethod;
        }

        public bool IsDead() => HP <= 0;

        public void TakeDamage(float damage) {
            HP -= damage;
            OnHpChange?.Invoke();
        }
        public void RestoreHealth(float heal) {
            HP += heal;
            OnHpChange?.Invoke();
        }
        public void HealToMaxHP() => HP = MaxHP;
    }
}