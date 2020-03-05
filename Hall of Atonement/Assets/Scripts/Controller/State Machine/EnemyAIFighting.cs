using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIFighting : EnemyAIStateMachine
{
    private float timer = 3f;

    private Coroutine fightingRoutine;

    private IEnemyAttackType enemyAttackType;


    private void Start()
    {
        enemyAttackType = gameObject.GetComponent<IEnemyAttackType>();
    }


    public override void Fighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        if (fightingRoutine == null)
        {
            fightingRoutine = StartCoroutine(FightingEnumerator(enemyAI, focusTarget));
        }
    }


    public override void Patrolling(EnemyAI enemyAI)
    {
        if (fightingRoutine != null)
        {
            StopCoroutine(fightingRoutine);
            fightingRoutine = null;
        }
        enemyAI.Combat.targetToAttack = null;
        enemyAI.EnemyAIStateMachine = enemyAI.EnemyAIPatrolling;
        enemyAI.EnemyAIStateMachine.Patrolling(enemyAI);
    }


    private IEnumerator FightingEnumerator(EnemyAI enemyAI, GameObject focusTarget) //Хочу потом переопределять этот метод, в зависимости от типа атаки
    {
        //Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;


        float timerCounter = timer;

        while (timerCounter > 0f
            && focusTarget != null
            && (Vector2.Distance(focusTarget.transform.position, transform.position) <= enemyAI.MyEnemyStats.ViewingRadius))
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
