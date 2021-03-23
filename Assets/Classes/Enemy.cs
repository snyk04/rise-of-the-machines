using UnityEngine;

namespace Classes
{
    public class Enemy : Person
    {
        public Enemy(float maxHealth, float moveSpeed, Transform transform, Animator animator) : base(maxHealth, moveSpeed, transform, animator) { }

        public override void Die()
        {
            //TODO: animation
            Object.Destroy(Transform.gameObject);
        }
    }
}
