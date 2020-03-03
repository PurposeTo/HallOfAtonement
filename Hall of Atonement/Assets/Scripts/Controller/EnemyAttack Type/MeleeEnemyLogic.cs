using UnityEngine;


[RequireComponent(typeof(MeleeCombat))]
public class MeleeEnemyLogic : CharacterCombat, IEnemyAttackType
{
    private IMelee meleeAttacker;

    private protected override void Start()
    {
        base.Start();
        meleeAttacker = (IMelee)Attacker;
    }

    void IEnemyAttackType.AttackTheTarget(GameObject target)
    {
        if (Vector2.Distance(target.transform.position, transform.position) <= meleeAttacker.MeleeAttackRange)
        {
            targetToAttack = target;
            PreAttack();
        }
    }

    Vector2 IEnemyAttackType.GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget)
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
