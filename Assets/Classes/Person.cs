using System.Collections.Generic;
using Classes.TryInHierarchie;
using UnityEngine;
using Type = Classes.TryInHierarchie.Characteristic.Type;

namespace Classes {
    public abstract class Person : IDamageable, IHasCharacteristics {

        public Dictionary<Type, Characteristic> Stats { get; private set; } = new Dictionary<Type, Characteristic>();
        public Health Health { get; }
        public SpeedCharacteristic MoveSpeed { get; protected set; }
        public ArmorCharacteristic Armor { get; protected set; }
        public Transform Transform { get; }
        public Animator Animator { get; }

        protected Person(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator) {
            Health = new Health(maxHealth, Die);
            MoveSpeed = new SpeedCharacteristic(moveSpeed);
            Armor = new ArmorCharacteristic(armor);
            Transform = transform;
            Animator = animator;
        }

        protected virtual void Die() { }

        protected virtual void Attack() { }

        public void TakeDamage(float damage) {
            Health.TakeDamage(damage);
        }

        public void RestoreHealth(float heal) {
            Health.RestoreHealth(heal);
        }
    }
}