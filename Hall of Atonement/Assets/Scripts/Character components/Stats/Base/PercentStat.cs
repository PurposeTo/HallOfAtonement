using UnityEngine;

[System.Serializable]
public class PercentStat : Stat
{
    //Этот класс всегда в диапазоне от 0 до 1
    
    public PercentStat() : this(0f) { }

    public PercentStat(float baseValue) : base(baseValue, 0f, 1f) { }


    //Вернуть значение по закону убывающей полезности
    public override float GetValue()
    {
        float finalValue = 1f - baseValue;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            finalValue *= (1f - statModifiers[i].GetModifierValue());
        }
        finalValue = 1f - finalValue;

        return Mathf.Clamp(finalValue, minValue, maxValue);
    }


    public override void AddModifier(ICharacteristicModifier<float> modifier)
    {
        if (modifier.GetModifierValue() > 1f)
        {
            Debug.LogWarning("Too much Percent modifier to add!");
        }

        base.AddModifier(modifier);
    }


    public override void RemoveModifier(ICharacteristicModifier<float> modifier)
    {
        if (modifier.GetModifierValue() > 1f)
        {
            Debug.LogWarning("You remove too much Percent modifier!");
        }

        base.RemoveModifier(modifier);
    }
}
