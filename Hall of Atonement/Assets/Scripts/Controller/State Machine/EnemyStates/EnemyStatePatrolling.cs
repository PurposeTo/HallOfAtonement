using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatePatrolling : EnemyAIStateMachine
{
    private Coroutine patrollingRoutine;


    public override void Fighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        if (patrollingRoutine != null)
        {
            StopCoroutine(patrollingRoutine);
            patrollingRoutine = null;
        }

        enemyAI.EnemyAIStateMachine = enemyAI.EnemyStateFighting;
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
        //Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;

        //Бесконечный цикл потому, что мы можем бесконечно "шастать" по уровню
        while (true)
        {
            //Патрулировать
            enemyAI.InputVector = Vector2.zero;
            yield return null;

            //Когда закончим, вызвать метод, говорящее о том, что мы закончили
            enemyAI.DecideWhatToDo();

        }

        //Выход из цикла есть только при переключении State
        //patrollingRoutine = null; 
    }
}
