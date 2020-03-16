using UnityEngine;

public class DamageReducerFactory : IDamageReducerFactory
{
    public IDamageReducerProduct CreateDamageReducerProduct(DamageType damageType)
    {
        if (damageType is PhysicalDamage) { return new PhysicalReducer(); }
        else if (damageType is EffectDamage) { return new EffectDamageReducer((EffectDamage)damageType); }
        else
        {
            Debug.LogError("Try to use unknown damage type: " + damageType);
            return null;
        }
    }
}
