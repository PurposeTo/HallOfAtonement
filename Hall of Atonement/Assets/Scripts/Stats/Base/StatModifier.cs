using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModifier : IStatModifier
{
    public StatModifier() : this(0) { }
    public StatModifier(float value) { ModifierValue = value; }

    public float ModifierValue { get; set; }
}
