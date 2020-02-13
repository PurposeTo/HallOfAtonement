using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStats : UnitStats
{
    public override void TakeDamage(CharacterStats killerStats, float damage)
    {
        damage = ReduceDamageFromArmor(damage);

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (CurrentHealthPoint - damage <= 0f) //Если из за полученного урона здоровье будет равно или ниже нуля
        {
            CurrentHealthPoint = 0f;

            Die(killerStats);
        }
        else
        {
            CurrentHealthPoint -= damage;
        }
    }


    public override void Die(CharacterStats killerStats)
    {
        Debug.Log(transform.name + " Разрушен!");
        Destroy(gameObject);
    }
}
