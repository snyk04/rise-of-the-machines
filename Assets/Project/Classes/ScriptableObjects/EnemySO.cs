using UnityEngine;

namespace Project.Classes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy", order = 1)]
    public class EnemySO : ScriptableObject
    {
        [Header("Main settings")]
        public float health;
        public float armor;
        public float moveSpeed;

        [Header("Patrol settings")]
        public float fieldOfView;
        public float viewDistance;

        [Header("Fight settings")]
        public float fightStartDistance;
        public float fightStopDistance;
        public float damagePerHit;
        public float attackInterval;
    }
}
