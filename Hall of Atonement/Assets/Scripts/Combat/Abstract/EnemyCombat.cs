using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombat : CharacterCombat, IEnemyAttackType
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }


    //private protected EnemyAI enemyAI;
    //private protected EnemyStats myEnemyStats;

    private protected virtual float AttackRange { get; set; }

    private protected abstract Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget);

    private protected override void Start()
    {
        base.Start();
        EnemyPresenter = (EnemyPresenter)CharacterPresenter;
    }


    void IEnemyAttackType.GetEnemyFightingLogic(EnemyAI enemyAI, GameObject focusTarget)
    {
        enemyAI.InputVector = GetMovingVectorOnFighting(enemyAI, focusTarget);

        // Как/когда нужно атаковать?
        PreAttack(focusTarget);
    }


    private protected override void PreAttack(GameObject target)
    {
        if (Vector2.Distance(target.transform.position, transform.position)
            <= AttackRange)
        {
            base.PreAttack(target);
        }
        else
        {
            targetToAttack = null;
        }
    }
}
