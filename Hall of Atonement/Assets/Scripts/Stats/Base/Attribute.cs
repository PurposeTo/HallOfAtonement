using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute
{
    [SerializeField]
    private int baseValue;    // Starting value

    private List<int> attributeModifiers = new List<int>();


    public Attribute() : this(0) { }

    public Attribute(int baseValue)
    {
        this.baseValue = Mathf.Clamp(baseValue, 0, int.MaxValue); //Атрибут должен быть только положительный!
    }


	public int GetValue()
	{
		int finalValue = baseValue;
		attributeModifiers.ForEach(x => finalValue += x);
		return finalValue;
	}


	public float GetBaseValue()
	{
		return baseValue;
	}

	// Add a new modifier to the list
	public void AddModifier(int modifier)
	{
		if (modifier != 0)
			attributeModifiers.Add(modifier);
	}

	// Remove a modifier from the list
	public void RemoveModifier(int modifier)
	{
		if (modifier != 0)
			attributeModifiers.Remove(modifier);
	}
}
