using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RangedShootCombat))]
[RequireComponent(typeof(Lifesteal))]
public class VampireStats : PlayerStats
{
    // Вампир не ограничен максимальным количеством ХП
    public override void Healing(float amount)
    {
        CurrentHealthPoint += amount;

        healthBar.IncreaseHealthBar();
    }
}
