using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIPatrolling : EnemyAIStateMachine
{
    private Coroutine patrollingRoutine;


    public override void Fighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        if (patrollingRoutine != null)
        {
            patrollingRoutine = null;
            StopCoroutine(patrollingRoutine);
        }

        enemyAI.EnemyAIStateMachine = enemyAI.EnemyAIHunting;
        enemyAI.EnemyAIStateMachine.Fighting(enemyAI, focusTarget);
    }

    public override void Patrolling(EnemyAI enemyAI) //Хочу потом переопределять этот метод, в зависимости от типа Enemy
    {
        if (patrollingRoutine == null)
        {
            patrollingRoutine = StartCoroutine(patrollingEnumerator(enemyAI));
        }
    }

    private IEnumerator patrollingEnumerator(EnemyAI enemyAI)
    {
        //Патрулировать
        enemyAI.InputVector = Vector2.zero;


        //Обязательно подождать кадр, что бы избежать багов при бесконечном цикле!
        yield return null;
        patrollingRoutine = null;
        //Когда закончим, вызвать метод, говорящее о том, что мы закончили
        enemyAI.DecideWhatToDo();

    }
}
