using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuardianAI : EnemyAI
{
    private protected override GameObject SearchingTarget()
    {
        GameObject target = null;

        if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= LookRadius) 
        {
            target = GameManager.instance.player;
        }
        else
        {
            target = null;
        }


        //Если цель найдена, идти к ней И атаковать ее, если она достаточно близко

        //плавное сглаживание вектора
        inputVector = Vector2.MoveTowards(inputVector, MoveToTarget(target), 10f * Time.fixedDeltaTime);

        if (target != null)
        {
            combat.SearchingTargetToAttack(target);
        }

        return target;
    }

}
