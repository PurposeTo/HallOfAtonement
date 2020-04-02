using System;
using UnityEngine;

public delegate void ChangeCurrentHealth();
public abstract class UnitStats : MonoBehaviour
{
    public event ChangeCurrentHealth OnChangedCurrentHealth;

    private protected virtual float BaseMaxHealthPoint { get; } = 100f; //базовое значение максимального кол-ва здоровья

    private protected virtual float BaseArmor { get; }

    private protected virtual float basePoisonResistanceValue { get; }
    private protected virtual float baseBleedingResistanceValue { get; }
    private protected virtual float baseFireResistanceValue { get; }
    private protected virtual float baseIceResistanceValue { get; }

    public Stat maxHealthPoint;
    public float CurrentHealthPoint { get; private protected set; }
    public Stat armor; //Нет базового значения
    public PercentStat fireResistance;
    public PercentStat iceResistance;
    public PercentStat poisonResistance;
    public PercentStat bleedingResistance;

    private IDamageReducerFactory damageReducerFactory = new DamageReducerFactory();


    private protected virtual void Awake()
    {
        StatInitialization();
        CurrentHealthPoint = maxHealthPoint.GetValue();
    }


    private protected virtual void StatInitialization()
    {
        maxHealthPoint = new Stat(BaseMaxHealthPoint);
        armor = new Stat(BaseArmor);


        poisonResistance = new PercentStat(basePoisonResistanceValue);
        bleedingResistance = new PercentStat(baseBleedingResistanceValue);
        fireResistance = new PercentStat(baseFireResistanceValue);
        iceResistance = new PercentStat(baseIceResistanceValue);
    }


    private protected void ReportUpdateHealthValue()
    {
        OnChangedCurrentHealth?.Invoke();
    }


    public virtual float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked, bool canEvade = true)
    {
        //снизить урон от определенного типа урона
        IDamageReducerProduct damageReducer = damageReducerFactory.CreateDamageReducerProduct(damageType);
        damage = damageReducer.ReduceDamage(this, damage, out isBlocked);

        damage = Mathf.Clamp(damage, 0f, float.MaxValue);
        //DisplayDamageTaken(isEvaded, isBlocked, damageType, damage);

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

        if (!isBlocked)
        {
            ReportUpdateHealthValue();
        }

        return damage;
    }


    //private protected virtual void DisplayDamageTaken(bool isEvaded, bool isBlocked, DamageType damageType, float damage)
    //{
    //    if (isBlocked)
    //    {
    //        Debug.Log(transform.name + " blocked the " + damageType);
    //    }
    //    else
    //    {
    //        Debug.Log(transform.name + " takes " + damage + " " + damageType);
    //    }
    //}


    public abstract void Die(CharacterStats killerStats);
}
