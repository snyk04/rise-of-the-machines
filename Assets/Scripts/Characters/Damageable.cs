using Classes;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    private float maxHealth;
    private IDamageable damageble;

    public void Initialize(float maxHealth, float moveSpeed, float armor, Transform transform, Animator animator = null)
    {
        this.maxHealth = maxHealth;
        damageble = new Enemy(maxHealth, armor, moveSpeed, transform, animator);
    }

    public void TakeDamage(float damage)
    {
        damageble = damageble ?? new Breakable(maxHealth, transform);
        damageble.Health.TakeDamage(damage);
    }
}