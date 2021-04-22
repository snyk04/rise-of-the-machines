using UnityEngine;

namespace Classes.TryInHierarchie {
    public class HealthCharacteristic : Characteristic {
        public delegate void HealthZero();
        public delegate void HpChange();

        private event HealthZero OnHealthZero;
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
                    OnHealthZero?.Invoke();
                }
            }
        }

        public HealthCharacteristic(float maxHp, HealthZero onHealthZeroMethod) : base(maxHp) {
            MaxHP = maxHp;
            OnHealthZero += onHealthZeroMethod;
        }

        public bool IsDead() => HP <= 0;

        public void TakeDamage(float damage) {
            if (IsDead()) {
                return;
            }
            HP -= damage;
            OnHpChange?.Invoke();
        }
        public void RestoreHealth(float heal) {
            HP += heal;
            OnHpChange?.Invoke();
        }
        public void HealToMaxHP() => RestoreHealth(MaxHP);
    }
}