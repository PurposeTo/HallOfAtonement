using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFighting : EnemyAIStateMachine
{
    private GameObject focusTarget; //Цель, на которой враг в данный момент сфокусирован

    private float timer = 3f;

    private Coroutine fightingRoutine;

    private IEnemyAttackType enemyAttackType;


    private void Start()
    {
        enemyAttackType = gameObject.GetComponent<IEnemyAttackType>();
    }


    private protected override void StopTheAction(EnemyAI enemyAI)
    {
        if (fightingRoutine != null)
        {
            StopCoroutine(fightingRoutine);
            fightingRoutine = null;
        }
        enemyAI.EnemyPresenter.Combat.targetToAttack = null;
    }


    public override void Fighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        if (fightingRoutine == null)
        {
            this.focusTarget = focusTarget;

            fightingRoutine = StartCoroutine(FightingEnumerator(enemyAI));
        }
        else
        {
            //Если цель слишком далеко или ее вообше нет, то сменить цель на новую предложенную
            if (this.focusTarget == null || Vector2.Distance(this.focusTarget.transform.position, transform.position) <= enemyAI.EnemyPresenter.MyEnemyStats.ViewingRadius)
            {
                this.focusTarget = focusTarget;
            }
        }
    }


    public override void SeekingBattle(EnemyAI enemyAI)
    {
        // Если цель еще жива, то стоит преследовать ее
        if (focusTarget == null)
        {
            StopTheAction(enemyAI);
            enemyAI.EnemyAIStateMachine = enemyAI.EnemyStatePatrolling;
            enemyAI.EnemyAIStateMachine.SeekingBattle(enemyAI);
        }
        else
        {
            fightingRoutine = StartCoroutine(FightingEnumerator(enemyAI));
        }
    }


    private IEnumerator FightingEnumerator(EnemyAI enemyAI)
    {
        //Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;

        float timerCounter = timer;

        while (timerCounter > 0f && focusTarget != null)
        {
            enemyAttackType.GetEnemyFightingLogic(enemyAI, focusTarget);

            yield return null;
            timerCounter -= Time.deltaTime;
        }

        fightingRoutine = null;
        //Когда закончим, вызвать метод, говорящее о том, что мы закончили
        enemyAI.DecideWhatToDo();
    }
}
