using Project.Scripts;
using UnityEngine;

namespace Project.Classes.Damagable
{
    public class Enemy : Person
    {
        public Transform Transform { get; private set; }
        public Animator Animator { get; private set; }

        public Enemy(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator) : base(
            maxHealth, moveSpeed, armor) {
            Transform = transform;
            Animator = animator;
        }

        protected override void Die()
        {
            GameState.Instance.RemoveTriggeredEnemies(1);
            Object.Destroy(Transform.gameObject);
        }
    }
}
