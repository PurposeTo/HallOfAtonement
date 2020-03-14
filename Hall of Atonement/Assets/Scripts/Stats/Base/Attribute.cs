using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute
{
    [SerializeField]
    private int baseValue;    // Starting value

	private protected List<IParameterModifier<int>> attributeModifiers = new List<IParameterModifier<int>>();

	public Attribute() : this(0) { }

    public Attribute(int baseValue)
    {
        this.baseValue = Mathf.Clamp(baseValue, 0, int.MaxValue); //Атрибут должен быть только положительный!
    }


	public int GetValue()
	{
		int finalValue = baseValue;

		for (int i = 0; i < attributeModifiers.Count; i++)
		{
			finalValue += attributeModifiers[i].ModifierValue;
		}

		if (finalValue < 0) { return 0; }
		else { return finalValue; }
	}


	public float GetBaseValue()
	{
		return baseValue;
	}

    public virtual void AddModifier(IParameterModifier<int> modifier)
    {
        attributeModifiers.Add(modifier);
    }


    public virtual void RemoveModifier(IParameterModifier<int> modifier)
    {
        attributeModifiers.Remove(modifier);
    }


    public virtual void ChangeBaseValue(int newBaseValue)
    {
        baseValue = newBaseValue;
    }
}
