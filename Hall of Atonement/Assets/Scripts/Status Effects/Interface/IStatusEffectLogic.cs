public interface IStatusEffectLogic
{
}

public interface IAttackModifier : IStatusEffectLogic
{
    void ApplyAttackModifier(float damage);
}

public interface IDefenseModifier : IStatusEffectLogic
{

}

public interface IStatsModifier : IStatusEffectLogic
{

}

public interface IDamageLogic : IStatusEffectLogic
{
    void DoStatusEffectDamage(UnitStats targetStats, CharacterStats killerStats, DamageType damageType);

    void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount);
}

public interface IHealLogic : IStatusEffectLogic
{

}