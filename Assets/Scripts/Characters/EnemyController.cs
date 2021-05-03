using CharactersAndObjects;
using Classes.ScriptableObjects;
using UnityEngine;

namespace Characters
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemySO enemy;
        [SerializeField] private Animator animator;

        public Animator Animator => animator;

        private Damageable damageable;

        private void Awake()
        {
            damageable = GetComponent<Damageable>();
            damageable.Initialize(enemy.health, enemy.moveSpeed, enemy.armor, transform, animator);
        }

        public EnemySO GetEnemySo() => enemy;
    }
}
