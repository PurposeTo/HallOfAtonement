public interface IStatusEffectLogic
{
}

public interface IAttackModifier : IStatusEffectLogic
{
    void ApplyAttackModifier(float damage, int mastery);
}

public interface IDefenseModifier : IStatusEffectLogic
{
    void ApplyDefenseModifier(CharacterStats killerStats, DamageType damageType, float damage, out bool isEvaded, out bool isBlocked);
}

public interface IStatModifier : IStatusEffectLogic
{
    float GetModifierValue();
}

public interface IDamageLogic : IStatusEffectLogic
{
    void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownersStats);

    void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount);
}

public interface IHealLogic : IStatusEffectLogic
{

}