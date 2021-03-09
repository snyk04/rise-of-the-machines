using Classes;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private IDamageable damageable;

    public void Initialize(float maxHealth, float moveSpeed, Transform transform, Animator animator = null)
    {
        damageable = new Enemy(maxHealth, moveSpeed, transform, animator);
    }

    public void TakeDamage(float damage)
    {
        damageable = damageable ?? new Breakable(maxHealth, transform);
        damageable.TakeDamage(damage);
    }
}
