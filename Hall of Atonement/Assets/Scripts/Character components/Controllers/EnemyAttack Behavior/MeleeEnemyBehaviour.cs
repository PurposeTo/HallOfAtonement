﻿using UnityEngine;

[RequireComponent(typeof(IMelee))]
public class MeleeEnemyBehaviour : MonoBehaviour, IEnemyAttackBehavior
{
    private float AttackRange;

    private void Start()
    {
        MeleeWeapon meleeWeapon = gameObject.GetComponentInChildren<MeleeWeapon>();

        AttackRange = meleeWeapon.GetMeleeAttackRadius() + meleeWeapon.WeaponAttackPoint.localPosition.magnitude;
    }


    Vector2 IEnemyAttackBehavior.GetMovingVectorOnFighting(GameObject focusTarget)
    {
        Vector2 newInputVector = Vector2.zero;

        if (focusTarget != null)
        {
            Vector2 direction = focusTarget.transform.position - transform.position; //Расстояние до цели

            //Если мы движемся, то двигаться пока расстояние до цели > minStopRadius
            //if (direction.magnitude > meleeWeapon.MeleeAttackRadius)
            if (direction.magnitude > AttackRange) // Враг вообще не атакует
            {
                newInputVector = direction.normalized;
            }
            else
            {
                newInputVector = Vector2.zero;
            }
        }

        return newInputVector;
    }


    float IEnemyAttackBehavior.GetAttackRange()
    {
        return AttackRange;
    }
}
