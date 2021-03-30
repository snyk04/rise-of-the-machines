using Classes;
using Classes.TryInHierarchie;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    private Health health;

    public void Initialize(float maxHealth, float moveSpeed, Transform transform, Animator animator = null)
    {
        health = new Enemy(maxHealth, moveSpeed, transform, animator).Health;
    }

    public void TakeDamage(float damage)
    {
        health = health ?? new Breakable(maxHealth, transform).Health;
        health.TakeDamage(damage);
    }
}
