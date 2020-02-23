interface IStatusEffectLogic
{
}

interface IAttackModifier : IStatusEffectLogic
{ 

}

interface IDefenseModifier : IStatusEffectLogic
{

}

interface IStatsModifier : IStatusEffectLogic
{

}

interface IDamageLogic : IStatusEffectLogic
{
    void StatusEffectDamage(UnitStats targetStats, CharacterStats killerStats, DamageType damageType);

    void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount);
}

interface IHealLogic : IStatusEffectLogic
{

}