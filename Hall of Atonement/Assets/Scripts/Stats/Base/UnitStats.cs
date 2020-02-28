using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitStats : MonoBehaviour
{
    private protected virtual float BaseMaxHealthPoint { get; } = 50f; //базовое значение максимального кол-ва здоровья
    public Stat maxHealthPoint;
    public float CurrentHealthPoint { get; private protected set; }
    public Stat armor; //Нет базового значения
    public PercentStat fireResistance;
    public PercentStat iceResistance;
    public PercentStat poisonResistance;
    public PercentStat bleedingResistance;

    IDamageReducerAndStatusEffectFactory damageReducerFactory;


    private protected virtual void Awake()
    {
        StatInitialization();
        CurrentHealthPoint = maxHealthPoint.GetValue();
        damageReducerFactory = new DamageReducerAndStatusEffectFactory();
    }


    private protected virtual void StatInitialization()
    {
        maxHealthPoint = new Stat(BaseMaxHealthPoint);
    }


    public virtual float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage, out bool isEvaded)
    {
        isEvaded = false;

        //снизить урон от определенного типа урона
        IDamageReducerProduct damageReducer = damageReducerFactory.CreateDamageReducerProduct(damageType);
        damage = damageReducer.ReduceDamage(this, damage);

        Debug.Log(transform.name + " takes " + damage + " " + killerStats.DamageType);

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
