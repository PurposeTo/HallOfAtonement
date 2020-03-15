public interface IStatusEffectLogic
{
}


public interface IAttackModifier : IStatusEffectLogic
{
    void ApplyAttackModifier(float damage, int mastery);
}


public interface IDefenseModifier : IStatusEffectLogic
{
    void ApplyDefenseModifier(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked);
}


public interface IParameterModifier<T> : IStatusEffectLogic
{
    T ModifierValue { get; set; }
}


public interface IDamageLogic : IStatusEffectLogic
{
    void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats);

    void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount);
}


public interface IHealLogic : IStatusEffectLogic
{

}
