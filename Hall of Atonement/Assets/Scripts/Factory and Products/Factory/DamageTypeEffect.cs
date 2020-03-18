using UnityEngine;

public class DamageTypeEffect
{
    public DamageTypeEffect(EffectDamage damageType, GameObject target, CharacterStats ownerStats, float amplificationAmount)
    {
        if (damageType is FireDamage)
        {
            new StatusEffectFactory<Burn>(target, ownerStats, amplificationAmount);
        }
        else if (damageType is IceDamage)
        {
            new StatusEffectFactory<Freeze>(target, ownerStats, amplificationAmount);
        }
    }
}
