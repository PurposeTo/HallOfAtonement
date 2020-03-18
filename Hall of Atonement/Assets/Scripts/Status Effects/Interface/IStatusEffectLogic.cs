using System;
using UnityEngine;

public interface IStatusEffectLogic
{

}


public abstract class HangingEffect : MonoBehaviour
{
    public abstract void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount);
}


public interface IAttackModifier : IStatusEffectLogic, ICloneable
{
    void ApplyAttackModifier(UnitStats targetStats, float damage, int mastery);
}


public interface IDefenseModifier : IStatusEffectLogic
{
    void ApplyDefenseModifier(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked);
}


public interface ICharacteristicModifier<T> : IStatusEffectLogic
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
