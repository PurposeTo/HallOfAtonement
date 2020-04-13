using System;

public interface IStatusEffectLogic
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
