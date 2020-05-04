using System.Collections.Generic;

public interface IWeapon
{
    void UseWeapon(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers);
}


public interface IMelee : IWeapon
{
    float GetMeleeAttackRadius();
}


public interface IRanged : IWeapon
{

}
