﻿using UnityEngine;

public class BoxStats : UnitStats
{
    public override void Die(CharacterStats killerStats)
    {
        Debug.Log(transform.name + " Разрушен!");
        Destroy(gameObject);
    }
}
