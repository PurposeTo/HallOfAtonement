interface IStatusEffectLogic
{
}

interface IAttackModifier : IStatusEffectLogic
{
    void ApplyAttackModifier();
}

interface IDefenseModifier : IStatusEffectLogic
{

}

interface IStatsModifier : IStatusEffectLogic
{

}

interface IDamageLogic : IStatusEffectLogic
{
    void DoStatusEffectDamage(UnitStats targetStats, CharacterStats killerStats, DamageType damageType);

    void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount);
}

interface IHealLogic : IStatusEffectLogic
{

}