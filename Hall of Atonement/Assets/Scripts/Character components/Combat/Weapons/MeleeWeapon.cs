using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : BaseWeapon, IMelee
{
    [SerializeField]
    private float meleeAttackRadius = .8f;

    public float GetMeleeAttackRadius()
    {
        return meleeAttackRadius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(WeaponAttackPoint.position, meleeAttackRadius);
    }


    void IWeapon.UseWeapon(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(WeaponAttackPoint.position, meleeAttackRadius);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            hitUnits[i].TryGetComponent(out UnitStats targetStats);

            if (targetStats != ownerStats) // Если мы попали не в себя
            {
                Debug.Log("Клинок " + ownerStats + " попал в: " + hitUnits[i].gameObject);


                if (targetStats != null)
                {
                    combat.DamageUnit.DoDamage(targetStats, ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);
                }
            }
        }


        combat.EnableAttackCooldown();
    }
}
