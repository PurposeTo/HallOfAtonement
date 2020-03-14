using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PercentStat : Stat
{
    //Этот класс всегда в диапазоне от 0 до 100.
    
    public PercentStat() : this(0f) { }

    public PercentStat(float baseValue) : base(baseValue, 0f, 1f) { }


    //Вернуть значение по закону убывающей полезности
    public override float GetValue()
    {
        float finalValue = 1f - baseValue;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            finalValue *= (1f - statModifiers[i].ModifierValue);
        }

        finalValue = 1f - finalValue;

        if (finalValue < minValue) { return minValue; }
        else if (finalValue > maxValue) { return maxValue; }
        else { return finalValue; }
    }


    public override void AddModifier(IParameterModifier<float> modifier)
    {
        if (modifier.ModifierValue > 1f)
        {
            Debug.LogWarning("Too much modifier to add!");
        }

        base.AddModifier(modifier);
    }


    public override void RemoveModifier(IParameterModifier<float> modifier)
    {
        if (modifier.ModifierValue > 1f)
        {
            Debug.LogWarning("Too much modifier!");
        }

        base.RemoveModifier(modifier);
    }
}
