using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PercentStat : Stat
{
    //Этот класс всегда в диапазоне от 0 до 100.
    
    public PercentStat() : this(0f) { }

    public PercentStat(float baseValue) : base(baseValue, 0f, 100f) { }


    //Вернуть значение по закону убывающей полезности
    public override float GetValue()
    {
        float finalValue = 100f - baseValue;

        statModifiers.ForEach(x => finalValue *= (100f - x));

        finalValue = 100f - finalValue;

        return finalValue >= 0f ? finalValue : 0f;
    }


    public override void AddModifier(float modifier)
    {
        if (modifier > 100f)
        {
            Debug.LogError("Too much modifier to add!");
            modifier = 100f;
        }
        base.AddModifier(modifier);
    }


    public override void RemoveModifier(float modifier)
    {
        if (modifier > 100f)
        {
            Debug.LogError("Too much modifier to add!");
            modifier = 100f;
        }
        base.RemoveModifier(modifier);
    }
}
