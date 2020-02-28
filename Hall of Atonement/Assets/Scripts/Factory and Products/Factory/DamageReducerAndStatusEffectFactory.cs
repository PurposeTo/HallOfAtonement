using UnityEngine;

public class DamageReducerAndStatusEffectFactory : IDamageReducerAndStatusEffectFactory
{
    public IDamageReducerProduct CreateDamageReducerProduct(DamageType damageType)
    {
        if (damageType is PhysicalDamage) { return new PhysicalReducer(); }
        else if (damageType is EffectDamage) { return new EffectDamageReducer((EffectDamage)damageType); }
        else
        {
            Debug.LogError("Try to use unknown damage type");
            return null;
        }
    }


    public IStatusEffectProduct CreateStatusEffectProduct(DamageType damageType)
    {
        if (damageType is FireDamage) { return new BurnEffect(); }
        //else if (damageType is IceDamage) { return new IceEffect(); }
        else { return null; }
    }
}
