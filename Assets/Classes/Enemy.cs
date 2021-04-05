using UnityEngine;

namespace Classes
{
    public class Enemy : Person
    {
        public Enemy(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator) : base(maxHealth, moveSpeed, armor, transform, animator) { }

        protected override void Die()
        {
            //TODO: animation
            Object.Destroy(Transform.gameObject);
        }
    }
}
