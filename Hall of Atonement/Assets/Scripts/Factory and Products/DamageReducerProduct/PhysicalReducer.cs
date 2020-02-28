public class PhysicalReducer : IDamageReducerProduct
{
    public float ReduceDamage(UnitStats targetStats, float damage)
    {
        return damage *= 1f - (0.04f * targetStats.armor.GetValue() / (0.94f + (0.04f * targetStats.armor.GetValue())));
    }
}
