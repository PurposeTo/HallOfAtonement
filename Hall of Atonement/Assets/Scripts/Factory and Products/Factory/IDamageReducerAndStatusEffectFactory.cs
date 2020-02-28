public interface IDamageReducerAndStatusEffectFactory
{
    IDamageReducerProduct CreateDamageReducerProduct(DamageType damageType);
    IStatusEffectProduct CreateStatusEffectProduct(DamageType damageType);
}
