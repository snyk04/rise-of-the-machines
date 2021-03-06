using Classes;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private IDamageable damageable;

    public void Initialize(float maxHealth, float moveSpeed, Transform transform)
    {
        damageable = new Enemy(maxHealth, moveSpeed, transform);
    }

    public void TakeDamage(float damage)
    {
        damageable = (damageable != null) ? damageable : new Breakable(maxHealth, transform);
        damageable.TakeDamage(damage);
    }
}
