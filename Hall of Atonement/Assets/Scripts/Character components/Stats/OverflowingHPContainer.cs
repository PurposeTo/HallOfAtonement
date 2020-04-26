using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverflowingHPContainer
{
    private const int overflowingHPCoefficient = 6;

    private float overflowingHealthPoints;

    private CharacteristicModifier<float> attackDamageForOverflowingHP = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> maxHPForOverflowingHP = new CharacteristicModifier<float>();


    public float GetOverflowingHealthPoints()
    {
        return overflowingHealthPoints;
    }


    public void SetOverflowingHealthPoints(float value)
    {
        if (value >= 0f)
        {
            overflowingHealthPoints = value;
        }
        else
        {
            overflowingHealthPoints = 0f;
        }
    }

}
