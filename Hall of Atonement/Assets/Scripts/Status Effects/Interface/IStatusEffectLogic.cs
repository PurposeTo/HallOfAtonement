﻿using System;
using UnityEngine;

public interface IStatusEffectLogic
{

}


public abstract class StatusEffect : MonoBehaviour
{
    private protected abstract ContainerStatusEffects StatusEffectType { get; }
    public abstract StatusEffectData StatusEffectData { get; }
}


public delegate void ChangeDurationStatusEffect(ActiveEffect activeStatusEffect,  float currentPercentDuration);
public abstract class ActiveEffect : StatusEffect
{
    public event ChangeDurationStatusEffect onChangeDurationStatusEffect;

    private protected CharacterStats ownerStats;

    private protected abstract float BaseDurationTime { get; }
    private protected float effectPower = 1f;

    private float currentDurationTime;



    private protected float MaxDurationTime; // Вычисляется только при навешивании эффекта
    private protected float currentPercentDurationTime;

    public virtual void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        // В дочерних классах в этой строке должно происходить изменение текущей длительности эффекта

        this.ownerStats = ownerStats;

        if (GetCurrentDurationTime() > MaxDurationTime)
        {
            MaxDurationTime = GetCurrentDurationTime();
        }

        SetCurrentPercentDurationValue();
        effectPower += amplificationAmount;
    }

    private protected float GetCurrentDurationTime() { return currentDurationTime; }


    private protected void SetCurrentDurationTime(float value)
    {
        currentDurationTime = value;
        SetCurrentPercentDurationValue();
    }


    private void SetCurrentPercentDurationValue()
    {
        currentPercentDurationTime = currentDurationTime / MaxDurationTime;

        onChangeDurationStatusEffect?.Invoke(this, currentPercentDurationTime);
    }
}


public abstract class Passive : StatusEffect
{ 

}


public interface IAttackModifier : IStatusEffectLogic, ICloneable
{
    void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery, bool isCritical = false);
}


public interface IDefenseModifier : IStatusEffectLogic
{
    void ApplyDefenseModifier(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked);
}


public interface ICharacteristicModifier<T>// : IStatusEffectLogic
{
    event ChangeCharacteristicModifier OnChangeCharacteristicModifier;

    T GetModifierValue();

    void SetModifierValue(T newValue);
}


public interface IDamageLogic : IStatusEffectLogic
{
    void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats);
}


public interface IHealLogic : IStatusEffectLogic
{

}
