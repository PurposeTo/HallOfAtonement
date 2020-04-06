using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour, IMelee
{
    public Transform weapon;

    public float MeleeAttackRadius { get; set; } = .8f;
    Transform IWeapon.AttackPoint => weapon;

    private void OnDrawGizmosSelected()
    {
        if (weapon == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(weapon.position, MeleeAttackRadius);
    }


    void IWeapon.Attack(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float criticalChance, float criticalMultiplie, float attackDamage, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        print(gameObject.name + " использует ближнюю атаку!");

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(weapon.position, MeleeAttackRadius);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            if (hitUnits[i].gameObject != gameObject && hitUnits[i].TryGetComponent(out UnitStats targetStats))
            {
                combat.DoDamage(targetStats, ownerStats, damageType, criticalChance, criticalMultiplie, attackDamage, ownerMastery, attackModifiers);
            }
        }
    }
}
