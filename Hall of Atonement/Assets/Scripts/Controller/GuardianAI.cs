using UnityEngine;


public class GuardianAI : _OldEnemyAI
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
        InputVector = Vector2.MoveTowards(InputVector, GetMovingVectorOnHunting(target), 10f * Time.fixedDeltaTime);

        if (target != null)
        {
            Combat.SearchingTargetToAttack(target);
        }

        return target;
    }

}
