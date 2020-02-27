using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private protected float baseValue;    // Starting value

    private protected List<float> statModifiers = new List<float>();

    public Stat() : this(0f) { }

    public Stat(float baseValue) :this(baseValue, 0f) { }

    public Stat(float baseValue, float minValue) : this(baseValue, minValue, float.MaxValue) { }

    public Stat(float baseValue, float minValue, float maxValue)
    {
        //Стата должна быть только положительной. Так же можно задать минимальное и максимальное значение
        this.baseValue = Mathf.Clamp(baseValue, minValue, maxValue);
    }


    public virtual float GetValue()
    {
        float finalValue = baseValue;
        statModifiers.ForEach(x => finalValue += x);

        return finalValue >= 0f ? finalValue : 0f;
    }


    public float GetBaseValue()
    {
        return baseValue;
    }


    public virtual void AddModifier(float modifier)
    {
        if (modifier != 0)
            statModifiers.Add(modifier);
    }


    public virtual void RemoveModifier(float modifier)
    {
        if (modifier != 0)
            statModifiers.Remove(modifier);
    }
}
