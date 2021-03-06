public interface IDamageable
{
    public bool IsDead();
    public delegate void OnHealthZero();
    public event OnHealthZero healthZero;

    // public float Health { get; private set; }
    public float MaxHealth { get; }

    public void Die();
    public void TakeDamage(float damage);
}
