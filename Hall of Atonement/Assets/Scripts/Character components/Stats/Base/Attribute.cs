using System.Collections.Generic;
using UnityEngine;


public delegate void ChangeAttributeBaseValue();
public delegate void ChangeAttributFinalValue();
[System.Serializable] public class Attribute
{
    public Attribute() : this(0) { }

    public Attribute(int baseValue)
    {
        this.baseValue = Mathf.Clamp(baseValue, 0, int.MaxValue); //Атрибут должен быть только положительный!
    }


    public event ChangeAttributeBaseValue OnChangeAttributeBaseValue;
    public event ChangeAttributFinalValue OnChangeAttributeFinalValue;

    private int baseValue;    // Starting value

    private protected List<ICharacteristicModifier<int>> attributeModifiers = new List<ICharacteristicModifier<int>>();

    private float massPerAtribute = 1f; // Шанс выпадения атрибута по умолчанию одинаковый


    public int GetValue()
    {
        int finalValue = baseValue;

        for (int i = 0; i < attributeModifiers.Count; i++)
        {
            finalValue += attributeModifiers[i].GetModifierValue();
        }

        if (finalValue < 0) { return 0; }
        else { return finalValue; }
    }


    public int GetBaseValue()
    {
        return baseValue;
    }

    public virtual void AddModifier(ICharacteristicModifier<int> modifier)
    {
        attributeModifiers.Add(modifier);

        modifier.OnChangeCharacteristicModifier += ReportUpdateFinalValue;

        ReportUpdateFinalValue();
    }


    public virtual void RemoveModifier(ICharacteristicModifier<int> modifier)
    {
        attributeModifiers.Remove(modifier);

        modifier.OnChangeCharacteristicModifier -= ReportUpdateFinalValue;

        ReportUpdateFinalValue();
    }


    public virtual void ChangeBaseValue(int baseValue)
    {
        this.baseValue = Mathf.Clamp(baseValue, 0, int.MaxValue);

        ReportUpdateBaseValue();
    }


    private void ReportUpdateFinalValue() // Сообщить об обновлении
    {
        OnChangeAttributeFinalValue?.Invoke();
    }


    private void ReportUpdateBaseValue()
    {
        OnChangeAttributeBaseValue?.Invoke();

        ReportUpdateFinalValue();
    }


    public void SetMassPerAtribute(float Value)
    {
        Debug.Log("Attribute mutated");
        massPerAtribute = Value;
    }


    public float GetMassPerAtribute()
    {
        return massPerAtribute;
    }
}
