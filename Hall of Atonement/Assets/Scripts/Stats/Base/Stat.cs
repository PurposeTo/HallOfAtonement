using System.Collections.Generic;
using UnityEngine;

public delegate void ChangeStat();
[System.Serializable] public class Stat
{
    public Stat() : this(0f) { }

    public Stat(float baseValue) :this(baseValue, 0f) { }

    public Stat(float baseValue, float minValue) : this(baseValue, minValue, float.MaxValue) { }

    public Stat(float baseValue, float minValue, float maxValue)
    {
        //Стата должна быть только положительной. Так же можно задать минимальное и максимальное значение

        this.minValue = Mathf.Clamp(minValue, 0f, float.MaxValue); //Минимальное значение должно быть больше или равно нулю
        this.maxValue = Mathf.Clamp(maxValue, minValue, float.MaxValue); //Максимальное значение должно быть больше минимального

        this.baseValue = Mathf.Clamp(baseValue, this.minValue, this.maxValue);
    }


    public event ChangeStat OnChangeStatBaseValue;
    public event ChangeStat OnChangeStatFinalValue;

    [SerializeField] private protected float baseValue;    // Starting value
    private protected float minValue;
    private protected float maxValue;

    private protected List<ICharacteristicModifier<float>> statModifiers = new List<ICharacteristicModifier<float>>();


    public virtual float GetValue()
    {
        float finalValue = baseValue;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            finalValue += statModifiers[i].GetModifierValue();
        }

        return Mathf.Clamp(finalValue, minValue, maxValue);
    }


    public float GetBaseValue()
    {
        return baseValue;
    }


    public virtual void AddModifier(ICharacteristicModifier<float> modifier)
    {
        statModifiers.Add(modifier);

        modifier.OnChangeCharacteristicModifier += ReportUpdateFinalValue;

        ReportUpdateFinalValue();
    }


    public virtual void RemoveModifier(ICharacteristicModifier<float> modifier)
    {
        statModifiers.Remove(modifier);

        modifier.OnChangeCharacteristicModifier -= ReportUpdateFinalValue;

        ReportUpdateFinalValue();
    }


    public virtual void ChangeBaseValue(float baseValue)
    {
        this.baseValue = Mathf.Clamp(baseValue, this.minValue, this.maxValue);

        ReportUpdateBaseValue();
    }


    private void ReportUpdateFinalValue() // Сообщить об обновлении
    {
        OnChangeStatFinalValue?.Invoke();
    }


    private void ReportUpdateBaseValue()
    {
        OnChangeStatBaseValue?.Invoke();

        ReportUpdateFinalValue();

    }
}
