using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    void Attack(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float criticalChance, float criticalMultiplie, float attackDamage, int ownerMastery, List<IAttackModifier> attackModifiers);

    Transform AttackPoint { get; }
}


public interface IMelee : IWeapon
{
    float MeleeAttackRadius { get; set; }
}


public interface IRanged : IWeapon
{

}
