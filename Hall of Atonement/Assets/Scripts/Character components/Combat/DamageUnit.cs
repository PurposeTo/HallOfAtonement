using System.Collections.Generic;
using UnityEngine;

public class DamageUnit
{
    private const float effectPowerConversionCoefficient = 9f;


    public void DoDamage(UnitStats targetStats, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        bool isEvaded = false;
        bool isBlocked = false;

        attackDamage = targetStats.TakeDamage(ownerStats, damageType, attackDamage, ref isEvaded, ref isBlocked, isCritical: isCritical, displayPopup: true);

        if (!isEvaded)
        {
            if (!isBlocked) // Если урон не был полностью заблокирован 
            {
                if (damageType is EffectDamage)
                {
                    new DamageTypeEffect((EffectDamage)damageType, targetStats.gameObject, ownerStats, attackDamage / effectPowerConversionCoefficient);
                }
            }


            /*
             * Есть ли модификатор атаки?
             * 
             * Если у цели есть модификатор атаки, который должен навесить дебафф, то сам модификатор проверяет, может ли он это сделать.
             */

            for (int i = 0; i < attackModifiers.Count; i++)
            {
                attackModifiers[i].ApplyAttackModifier(targetStats, damageType, attackDamage, ownerMastery, isCritical);
            }
        }
    }



}
