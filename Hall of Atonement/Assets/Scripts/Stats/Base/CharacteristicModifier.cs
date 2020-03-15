using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChangeCharacteristicModifier();
public class CharacteristicModifier<T> : ICharacteristicModifier<T>
{
    public CharacteristicModifier() : this(default) { }
    public CharacteristicModifier(T value) { modifierValue = value; }


    public event ChangeCharacteristicModifier OnChangeCharacteristicModifier;

    private T modifierValue;


    public T GetModifierValue() 
    {
        return modifierValue;
    }


    public void SetModifierValue(T newValue)
    {
        modifierValue = newValue;

        if (OnChangeCharacteristicModifier != null) OnChangeCharacteristicModifier();
    }
}
