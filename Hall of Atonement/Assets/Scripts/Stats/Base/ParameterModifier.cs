using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChangeParameterModifier();
public class ParameterModifier<T> : IParameterModifier<T>
{
    public ParameterModifier() : this(default) { }
    public ParameterModifier(T value) { modifierValue = value; }


    public event ChangeParameterModifier OnChangeParameterModifier;

    private T modifierValue;


    public T GetModifierValue() 
    {
        return modifierValue;
    }


    public void SetModifierValue(T newValue)
    {
        modifierValue = newValue;

        if (OnChangeParameterModifier != null) OnChangeParameterModifier();
    }
}
