using Classes.ScriptableObjects;
using UnityEngine;

namespace Characters
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemySO enemy;

        private Damageable damageable;

        private void Awake()
        {
            damageable = GetComponent<Damageable>();
            damageable.Initialize(enemy.health, enemy.moveSpeed, enemy.armor, transform);
        }

        public EnemySO GetEnemySO() => enemy;
    }
}
