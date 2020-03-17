using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCombat : MonoBehaviour
{
    public List<IAttackModifier> attackModifiers = new List<IAttackModifier>();


    //Статы врага нужны, что бы можно было нанести урон. Свои статы нужны, что бы можно было получить опыт
    public void DoDamage(UnitStats targetStats, CharacterStats ownerStats, DamageType damageType, float criticalChance, float criticalMultiplie, float attackDamage, int ownerMastery, List<IAttackModifier> attackModifiers) 
    {

        //Формула, которая повысит урон в случае, если скорость атак будет быстрее чем обновление кадров
        //float attackSpeedMultiplie = (Mathf.Abs(attackCooldown) / (1f / ownerStats.attackSpeed.GetValue())) + 1f;

        //float damage = attackSpeedMultiplie * attackDamage;
        float damage = attackDamage;

        //Если Крит. шанс больше нуля И если рандом говорит о том, что нужно критануть
        if (criticalChance > 0f && Random.Range(1f, 100f)
            <= criticalChance)
        {
            damage *= criticalMultiplie; //То увеличить урон
        }


        bool isEvaded = false;
        bool isBlocked = false;

        damage = targetStats.TakeDamage(ownerStats, damageType, damage, true, ref isEvaded, ref isBlocked);

        if (!isEvaded)
        {
            if (!isBlocked) //Если урон не был полностью заблокирован 
            {
                if (damageType is EffectDamage)
                {
                    StatusEffectFactory statusEffectFactory = new StatusEffectFactory();
                    statusEffectFactory.HangStatusEffect(damageType, targetStats, ownerStats);
                }
            }


            /*
             * Есть ли модификатор атаки?
             * 
             * Если у цели есть модификатор атаки, который должен навесить дебафф, то сам модификатор проверяет, может ли он это сделать.
             */



            for (int i = 0; i < attackModifiers.Count; i++)
            {
                attackModifiers[i].ApplyAttackModifier(damage, ownerMastery);
            }
        }

    }
}
