﻿using UnityEngine;

public class EffectDamageReducer : IDamageReducerProduct
{
    private EffectDamage effectDamage;

    public EffectDamageReducer(EffectDamage effectDamage) 
    {
        this.effectDamage = effectDamage;
    }

    public float ReduceDamage(UnitStats targetStats, float damage, out bool isBlocked)
    {
        isBlocked = false;

        float effectDamageResistance;
        if (effectDamage is FireDamage)
        {
            effectDamageResistance = targetStats.fireResistance.GetValue();
        }
        else if (effectDamage is IceDamage)
        {
            effectDamageResistance = targetStats.iceResistance.GetValue();
        }
        else if (effectDamage is PoisonDamage)
        {
            effectDamageResistance = targetStats.poisonResistance.GetValue();
        }
        else if (effectDamage is BleedingDamage)
        {
            effectDamageResistance = targetStats.bleedingResistance.GetValue();
        }
        else
        {
            Debug.LogError("Unknown damage type for reduce damage");
            effectDamageResistance = 0f;
        }

        if (effectDamageResistance == 1f)
        {
            isBlocked = true;
            return 0f;
        }

        return damage *= (1f - effectDamageResistance);
    }
}
