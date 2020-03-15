using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterModifier<T> : IParameterModifier<T>
{
    public ParameterModifier() : this(default) { }
    public ParameterModifier(T value) { ModifierValue = value; }

    public T ModifierValue { get; set; }
}
