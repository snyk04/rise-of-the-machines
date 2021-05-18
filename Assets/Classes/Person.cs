using System.Collections.Generic;
using Classes.TryInHierarchie;
using UnityEngine;
using Type = Classes.TryInHierarchie.Characteristic.Type;

namespace Classes {
    public abstract class Person : IDamageable, IHasCharacteristics {

        public Dictionary<Type, Characteristic> Stats { get; private set; } = new Dictionary<Type, Characteristic>();
        public HealthCharacteristic Health { get; }
        public SpeedCharacteristic MoveSpeed { get; protected set; }
        public ArmorCharacteristic PersonArmor { get; protected set; }

        protected Person(float maxHealth, float moveSpeed, float armor) {
            Health = new HealthCharacteristic(maxHealth, Die);
            MoveSpeed = new SpeedCharacteristic(moveSpeed);
            PersonArmor = new ArmorCharacteristic(armor);
            Stats.Add(Type.Health, Health);
            Stats.Add(Type.Speed, MoveSpeed);
            Stats.Add(Type.Armor, PersonArmor);
        }

        protected virtual void Die() { }

        protected virtual void Attack() { }

        public void TakeDamage(float damage) {
            var damageAccountingArmor = damage * (1 - ((ArmorCharacteristic) Stats[Type.Armor]).GetResistance());
            Health.TakeDamage(damageAccountingArmor);
        }

        public void RestoreHealth(float heal) {
            Health.RestoreHealth(heal);
        }
    }
}