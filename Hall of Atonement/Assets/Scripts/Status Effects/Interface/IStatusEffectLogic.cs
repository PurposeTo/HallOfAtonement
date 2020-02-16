interface IStatusEffectLogic
{
    void HangStatusEffect();
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
    void StatusEffectDamage(UnitStats targetStats, CharacterStats killerStats, FireDamage fireDamage);
}

interface IHealLogic : IStatusEffectLogic
{

}