using Project.Classes.Damagеable;
using Project.Classes.ScriptableObjects;
using Project.Scripts.CharactersAndObjects;
using UnityEngine;

namespace Project.Scripts.Characters
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
