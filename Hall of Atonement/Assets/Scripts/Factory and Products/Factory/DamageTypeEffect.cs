using UnityEngine;

public class DamageTypeEffect
{
    public DamageTypeEffect(EffectDamage damageType, UnitStats targetStats, CharacterStats ownerStats, float amplificationAmount)
    {
        if (damageType is FireDamage)
        {
            new StatusEffectFactory<Burn>(targetStats, ownerStats, amplificationAmount);
        }
        else if (damageType is IceDamage)
        {
            new StatusEffectFactory<Freeze>(targetStats, ownerStats, amplificationAmount);
        }
    }
}
