using UnityEngine;

namespace Classes
{
    public class Enemy : Person
    {
        public Enemy(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator) : base(maxHealth, moveSpeed, armor, transform, animator) { }

        protected override void Die()
        {
            GameState.Instance.RemoveTriggeredEnemies(1);
            Object.Destroy(Transform.gameObject);
        }
    }
}
