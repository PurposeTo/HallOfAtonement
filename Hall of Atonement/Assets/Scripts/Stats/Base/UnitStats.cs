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


    public abstract void TakeDamage(CharacterStats killerStats, float damage);
    public abstract void Die(CharacterStats killerStats);
}
