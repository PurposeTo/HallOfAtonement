using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour, IMelee
{
    public Transform attackPoint;

    public float MeleeAttackRadius { get; set; } = .8f;
    Transform IAttacker.AttackPoint => attackPoint;

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, MeleeAttackRadius);
    }


    void IAttacker.Attack(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float criticalChance, float criticalMultiplie, float attackDamage, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        print(gameObject.name + " использует ближнюю атаку!");

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(attackPoint.position, MeleeAttackRadius);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            if (hitUnits[i].gameObject != gameObject && hitUnits[i].TryGetComponent(out UnitStats targetStats))
            {
                combat.DoDamage(targetStats, ownerStats, damageType, criticalChance, criticalMultiplie, attackDamage, ownerMastery, attackModifiers);
            }
        }
    }
}
