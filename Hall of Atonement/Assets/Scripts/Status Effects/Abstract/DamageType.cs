public abstract class DamageType
{
    public float Damage { get; private set; }

    public DamageType(float damage)
    {
        Damage = damage;
    }
}