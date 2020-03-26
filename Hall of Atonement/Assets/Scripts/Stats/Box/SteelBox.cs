using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelBox : BoxStats
{
    private protected override float baseFireResistanceValue { get; } = 1f;

    private protected override float baseIceResistanceValue { get; } = 1f;

    private protected override float BaseArmor { get; } = 20f;
}
