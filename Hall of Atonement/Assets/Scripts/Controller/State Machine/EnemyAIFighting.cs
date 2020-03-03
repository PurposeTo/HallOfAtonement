using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIFighting : EnemyAIStateMachine
{
    private float timer = 3f;

    private Coroutine fightingRoutine;


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
            fightingRoutine = null;
            StopCoroutine(fightingRoutine);
        }

        enemyAI.EnemyAIStateMachine = enemyAI.EnemyAIPatrolling;
        enemyAI.EnemyAIStateMachine.Patrolling(enemyAI);
    }


    private IEnumerator FightingEnumerator(EnemyAI enemyAI, GameObject focusTarget) //Хочу потом переопределять этот метод, в зависимости от типа атаки
    {
        float timerCounter = timer;

        while (timerCounter > 0f
            && focusTarget != null
            && (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= enemyAI.MyEnemyStats.ViewingRadius))
        {

            //Если цель найдена, идти к ней И атаковать ее, если она достаточно близко

            //плавное сглаживание вектора
            enemyAI.InputVector = Vector2.MoveTowards(enemyAI.InputVector,
                GetMovingVectorOnHunting(enemyAI, focusTarget), 10f * Time.fixedDeltaTime);

            //Значит все хорошо и можно продолжить охотиться
            enemyAI.Combat.SearchingTargetToAttack(focusTarget);

            yield return null;
            timerCounter -= Time.deltaTime;
        }

        //Обязательно подождать кадр, что бы избежать багов при бесконечном цикле!
        yield return null;
        fightingRoutine = null;
        //Когда закончим, вызвать метод, говорящее о том, что мы закончили
        enemyAI.DecideWhatToDo();
    }


    private protected Vector2 GetMovingVectorOnHunting(EnemyAI enemyAI, GameObject focusTarget) // Логика, как стоит двигаться при атаке
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
