using Project.Classes.Characteristics;
using Project.Interfaces;
using UnityEngine;

namespace Project.Classes.Damagable {
    public class Breakable : IDamageable {
        public HealthCharacteristic Health { get; }

        public Transform Transform { get; }

        public Breakable(float maxHealth, Transform transform) {
            Health = new HealthCharacteristic(maxHealth, Die);
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