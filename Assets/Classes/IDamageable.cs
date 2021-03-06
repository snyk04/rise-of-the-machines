public interface IDamageable
{
    bool IsDead();

    // public float Health { get; private set; }
    float MaxHealth { get; }

    void Die();
    void TakeDamage(float damage);
}
