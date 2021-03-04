namespace Classes
{
    public abstract class Person
    {
        protected float health;

        protected void TakeDamage(float damage)
        {
            health -= damage;
        }
    }
}
