using CharactersAndObjects;
using Classes;
using Classes.ScriptableObjects;
using UnityEngine;

namespace Characters
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemySO enemySo;

        public EnemySO EnemySo => enemySo;
        public Enemy Enemy { get; private set; }

        private void Awake()
        {
            var damageable = GetComponent<DamageableController>();
            var animator = GetComponent<Animator>();
            damageable.Initialize(enemySo.health, enemySo.moveSpeed, enemySo.armor, transform, animator);
            Enemy = (Enemy) damageable.DamageableClass;
        }
    }
}
