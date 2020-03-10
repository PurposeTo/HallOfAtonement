using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFighting : EnemyAIStateMachine
{
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
        enemyAI.Combat.targetToAttack = null;
    }


    public override void Fighting(EnemyAI enemyAI)
    {
        if (fightingRoutine == null)
        {
            fightingRoutine = StartCoroutine(FightingEnumerator(enemyAI, enemyAI.FocusTarget));
        }
    }


    public override void SeekingBattle(EnemyAI enemyAI)
    {
        StopTheAction(enemyAI);

        enemyAI.EnemyAIStateMachine = enemyAI.EnemyStatePatrolling;
        enemyAI.EnemyAIStateMachine.SeekingBattle(enemyAI);
    }


    private IEnumerator FightingEnumerator(EnemyAI enemyAI, GameObject focusTarget)
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
