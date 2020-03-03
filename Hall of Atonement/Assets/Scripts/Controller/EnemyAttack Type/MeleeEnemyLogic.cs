using UnityEngine;


[RequireComponent(typeof(MeleeCombat))]
public class MeleeEnemyLogic : EnemyCombat, IEnemyAttackType
{
    private IMelee meleeAttacker;

    private protected override void Start()
    {
        base.Start();
        meleeAttacker = (IMelee)Attacker;
    }


    void IEnemyAttackType.GetEnemyFightingLogic(EnemyAI enemyAI, GameObject focusTarget)
    {
        enemyAI.InputVector = GetMovingVectorOnFighting(enemyAI, focusTarget);

        // Как/когда нужно атаковать?
        PreAttack(focusTarget);
    }


    private protected override void PreAttack(GameObject target)
    {
        if (Vector2.Distance(target.transform.position, transform.position) <= meleeAttacker.MeleeAttackRange)
        {
            base.PreAttack(target);
        }
        else
        {
            targetToAttack = null;
        }
    }


    private protected override Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        Vector2 newInputVector;


        Vector2 direction = (focusTarget.transform.position - transform.position); //Расстояние до цели


            //Если мы движемся, то двигаться пока расстояние до цели > minStopRadius
            if (direction.magnitude > meleeAttacker.MeleeAttackRange)
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
