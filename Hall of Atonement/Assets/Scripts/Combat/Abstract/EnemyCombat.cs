using UnityEngine;

public abstract class EnemyCombat : CharacterCombat, IEnemyAttackType
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }

    private protected virtual float AttackRange { get; set; }


    private protected override void Start()
    {
        base.Start();
        EnemyPresenter = (EnemyPresenter)CharacterPresenter;
    }


    private protected abstract Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget);


    void IEnemyAttackType.GetEnemyFightingLogic(EnemyAI enemyAI, GameObject focusTarget)
    {
        enemyAI.InputVector = GetMovingVectorOnFighting(enemyAI, focusTarget);

        // Как/когда нужно атаковать?
        if (Vector2.Distance(focusTarget.transform.position, transform.position) <= AttackRange)
        {
            PreAttack(focusTarget);
        }
    }
}
