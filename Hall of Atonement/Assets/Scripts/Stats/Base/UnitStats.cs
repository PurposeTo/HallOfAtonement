using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStats : MonoBehaviour
{
    private protected virtual float BaseMaxHealthPoint { get; } = 50f; //базовое значение максимального кол-ва здоровья
    public Stat maxHealthPoint;
    public float CurrentHealthPoint { get; private protected set; }
    public Stat armor; //Нет базового значения


    private protected virtual void Start()
    {
        StatInitialization();
    }


    private protected float ReduceDamageFromArmor(float damage)
    {
        damage *= 1f - (0.04f * armor.GetValue() / (0.94f + (0.04f * armor.GetValue())));

        return damage;
    }


    private protected virtual void StatInitialization()
    {
        maxHealthPoint = new Stat(BaseMaxHealthPoint);
        CurrentHealthPoint = maxHealthPoint.GetValue();
    }


    public virtual float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage)
    {
        /*if (damageType is PhysicalDamage)
        {

        }
        else if (damageType is EffectDamage)
        {

        }*/

        damage = ReduceDamageFromArmor(damage);

        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (CurrentHealthPoint - damage <= 0f) //Если из за полученного урона здоровье будет равно или ниже нуля
        {
            damage = CurrentHealthPoint; //Нанесенный урон = оставшемуся здоровью
            CurrentHealthPoint = 0f;

            Die(killerStats);
        }
        else
        {
            CurrentHealthPoint -= damage;
        }
        return damage;
    }


    public abstract void Die(CharacterStats killerStats);
}
