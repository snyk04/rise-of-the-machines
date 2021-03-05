using UnityEngine;

namespace Classes
{
    public class Enemy : Person
    {
        public Enemy(float maxHealth, float moveSpeed, Transform transform) : base(maxHealth, moveSpeed, transform) { }
    }
}
