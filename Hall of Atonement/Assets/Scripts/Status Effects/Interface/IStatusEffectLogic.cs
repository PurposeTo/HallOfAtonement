using System;
using UnityEngine;

public interface IStatusEffectLogic
{

}


public abstract class ItemHarding : MonoBehaviour
{

}


public interface IAttackModifier : IStatusEffectLogic, ICloneable
{
    void ApplyAttackModifier(float damage, int mastery);
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

    void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount);
}


public interface IHealLogic : IStatusEffectLogic
{

}
