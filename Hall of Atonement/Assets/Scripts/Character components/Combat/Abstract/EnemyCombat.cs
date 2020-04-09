using UnityEngine;

public abstract class EnemyCombat : CharacterCombat
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }

    private protected virtual float AttackRange { get; set; }


    private protected override void Start()
    {
        base.Start();
        EnemyPresenter = (EnemyPresenter)CharacterPresenter;
    }


    public abstract Vector2 GetMovingVectorOnFighting(GameObject focusTarget);


    public void GetEnemyFightingLogic(GameObject focusTarget)
    {
        // Как/когда нужно атаковать?
        if (Vector2.Distance(focusTarget.transform.position, transform.position) <= AttackRange)
        {
            PreAttack(focusTarget);
        }
    }
}
