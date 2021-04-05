using Classes.TryInHierarchie;
using UnityEngine;

namespace Classes {
    public class Breakable : IDamageable {
        public Health Health { get; }

        public Transform Transform { get; }

        public Breakable(float maxHealth, Transform transform) {
            Health = new Health(maxHealth, Die);
            Transform = transform;
        }

        private void Die() {
            //TODO: animation
            Object.Destroy(Transform.gameObject);
        }

        public void TakeDamage(float damage) {
            Health.TakeDamage(damage);
        }
    }
}