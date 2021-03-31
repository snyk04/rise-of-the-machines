public interface IDamageable {
    bool IsDead();

    float HP { get; }

    // void Die();
    void TakeDamage(float damage);
}