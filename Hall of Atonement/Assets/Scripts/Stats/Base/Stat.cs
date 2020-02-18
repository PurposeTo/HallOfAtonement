using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private float baseValue;    // Starting value

    private List<float> statModifiers = new List<float>();

    public Stat() : this(0f) { }

    public Stat(float baseValue) :this(baseValue, 0f) { }

    public Stat(float baseValue, float minValue) : this(baseValue, minValue, float.MaxValue) { }

    public Stat(float baseValue, float minValue, float maxValue)
    {
        //Стата должна быть только положительной. Так же можно задать минимальное и максимальное значение
        this.baseValue = Mathf.Clamp(baseValue, minValue, maxValue);
    }


    public float GetValue()
    {
        float finalValue = baseValue;
        statModifiers.ForEach(x => finalValue += x);
        return finalValue;
    }


    public float GetBaseValue()
    {
        return baseValue;
    }


    public void AddModifier(float modifier)
    {
        if (modifier != 0)
            statModifiers.Add(modifier);
    }


    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
            statModifiers.Remove(modifier);
    }
}
