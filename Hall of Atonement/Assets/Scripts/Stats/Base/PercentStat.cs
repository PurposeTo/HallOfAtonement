using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentStat : Stat
{
    //Этот класс всегда в диапазоне от 0 до 100.
    
    public PercentStat() : base(0f, 0f, 100f) { }

    public PercentStat(float baseValue) : base(baseValue, 0f, 100f) { }
}
