using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Attack(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers);

    Transform AttackPoint { get; }
}


public interface IMelee : IWeapon
{
    float MeleeAttackRadius { get; set; }
}


public interface IRanged : IWeapon
{

}
