using System;
using UnityEngine;

namespace Project.Classes.Characteristics {
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
                if (Math.Abs(Value - value) > float.Epsilon) {
                    OnHpChange?.Invoke();
                }
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
        }
        public void RestoreHealth(float heal) {
            HP += heal;
        }
        public void HealToMaxHP() => RestoreHealth(MaxHP);
    }
}