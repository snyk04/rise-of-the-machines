using UnityEngine;

namespace Classes.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "ScriptableObject/Enemy", order = 1)]
    public class EnemySO : ScriptableObject
    {
        public float health;
        public float moveSpeed;
        public float armor;
        public float fieldOfView;
        public float viewDistance;
        public float fightStartDistance;
        public float fightStopDistance;
    }
}
