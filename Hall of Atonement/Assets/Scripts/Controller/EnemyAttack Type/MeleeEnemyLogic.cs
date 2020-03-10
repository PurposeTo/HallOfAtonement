using UnityEngine;


[RequireComponent(typeof(MeleeCombat))]
public class MeleeEnemyLogic : EnemyCombat
{
    private IMelee meleeAttacker;


    private protected override void Start()
    {
        base.Start();
        meleeAttacker = (IMelee)Attacker;
        AttackRange = meleeAttacker.MeleeAttackRadius + meleeAttacker.AttackPoint.localPosition.magnitude;

    }


    private protected override Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        Vector2 newInputVector;


        Vector2 direction = focusTarget.transform.position - transform.position; //Расстояние до цели

        //Если мы движемся, то двигаться пока расстояние до цели > minStopRadius
        if (direction.magnitude > meleeAttacker.MeleeAttackRadius)
        {
            newInputVector = direction.normalized;
        }
        else
        {
            newInputVector = Vector2.zero;
        }


        return newInputVector;
    }
}
