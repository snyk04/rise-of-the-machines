using Classes;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private IDamageable damageable;

    public void Initialize(float maxHealth, float moveSpeed, Transform transform)
    {
        damageable = new Enemy(maxHealth, moveSpeed, transform);
    }

    public void TakeDamage(float damage)
    {
        damageable.TakeDamage(damage);
    }
}
