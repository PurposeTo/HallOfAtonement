using UnityEngine;

[RequireComponent(typeof(RangedCombat))]
public class RangedEnemyLogic : EnemyCombat, IEnemyAttackType
{
    void IEnemyAttackType.GetEnemyFightingLogic(EnemyAI enemyAI, GameObject focusTarget)
    {
        enemyAI.InputVector = GetMovingVectorOnFighting(enemyAI, focusTarget);

        // Как/когда нужно атаковать?
        AttackTheTarget(focusTarget);
    }


    private protected override Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        Vector2 newInputVector;


        Vector2 direction = (focusTarget.transform.position - transform.position); //Расстояние до цели

        if (enemyAI.Rb2D.velocity.magnitude > 1f)
        {
            //Если мы движемся, то двигаться пока расстояние до цели > minStopRadius
            if (direction.magnitude > (enemyAI.MyEnemyStats.ViewingRadius / 2f))
            {
                newInputVector = direction.normalized;
            }
            else
            {
                newInputVector = Vector2.zero;
            }
        }
        else
        {
            //Если мы стоим, то стоять пока расстояние до цели не станет >= maxStopRadius
            if (direction.magnitude < ((enemyAI.MyEnemyStats.ViewingRadius / 4f) * 3f))
            {
                newInputVector = Vector2.zero;
            }
            else
            {
                newInputVector = direction.normalized;
            }
        }


        return newInputVector;
    }
}
