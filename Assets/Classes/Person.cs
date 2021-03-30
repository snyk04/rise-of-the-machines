using Classes.TryInHierarchie;
using UnityEngine;

namespace Classes {
    public abstract class Person {
        public Health Health { get; }
        public float MoveSpeed { get; }
        public Transform Transform { get; }
        public Animator Animator { get; }

        protected Person(float maxHealth, float moveSpeed, Transform transform, Animator animator) {
            Health = new Health(maxHealth, Die);
            MoveSpeed = moveSpeed;
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