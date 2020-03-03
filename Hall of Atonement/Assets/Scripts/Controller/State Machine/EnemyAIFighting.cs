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
            fightingRoutine = StartCoroutine(FightingLogic(enemyAI, focusTarget));
        }
    }


    public override void Patrolling(EnemyAI enemyAI)
    {
        if (fightingRoutine != null)
        {
            fightingRoutine = null;
            StopCoroutine(fightingRoutine);
        }

        enemyAI.EnemyAIStateMachine = enemyAI.EnemyAIPatrolling;
        enemyAI.EnemyAIStateMachine.Patrolling(enemyAI);
    }


    private IEnumerator FightingLogic(EnemyAI enemyAI, GameObject focusTarget) //Хочу потом переопределять этот метод, в зависимости от типа атаки
    {
        float timerCounter = timer;
        yield return null;
        timerCounter -= Time.deltaTime;


        while (timerCounter > 0f
            && focusTarget != null
            && (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= enemyAI.MyEnemyStats.ViewingRadius))
        {

            //Если цель найдена, идти к ней И атаковать ее, если она достаточно близко

            // Плавное сглаживание вектора. Как нужно двигаться в драке?
            enemyAI.InputVector = Vector2.MoveTowards(enemyAI.InputVector,
                enemyAttackType.GetMovingVectorOnFighting(enemyAI, focusTarget), 10f * Time.fixedDeltaTime);

            // Как/когда нужно атаковать?
            enemyAttackType.AttackTheTarget(focusTarget);

            yield return null;
            timerCounter -= Time.deltaTime;
        }

        //Обязательно подождать кадр, что бы избежать багов при бесконечном цикле!
        yield return null;
        fightingRoutine = null;
        //Когда закончим, вызвать метод, говорящее о том, что мы закончили
        enemyAI.DecideWhatToDo();
    }
}
