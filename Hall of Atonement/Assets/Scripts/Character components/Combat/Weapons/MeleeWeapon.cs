using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : BaseWeapon, IMelee
{
    public float MeleeAttackRadius { get; } = .8f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(WeaponAttackPoint.position, MeleeAttackRadius);
    }


    void IWeapon.UseWeapon(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        print(gameObject.name + " использует ближнюю атаку!");

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(WeaponAttackPoint.position, MeleeAttackRadius);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            hitUnits[i].TryGetComponent(out UnitStats targetStats);

            if (targetStats != ownerStats) // Если мы попали не в себя
            {
                Debug.Log("Клинок " + ownerStats.gameObject + "попал в: " + targetStats.gameObject);


                if (targetStats != null)
                {
                    combat.DamageUnit.DoDamage(targetStats, ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);
                }
            }
        }


        combat.EnableAttackCooldown();
    }
}
