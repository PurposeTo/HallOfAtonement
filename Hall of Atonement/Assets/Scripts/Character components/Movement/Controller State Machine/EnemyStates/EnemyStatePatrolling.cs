using System.Collections;
using UnityEngine;

public class EnemyStatePatrolling : EnemyStateMachine
{
    private Coroutine patrollingRoutine;

    private protected override void StopTheAction(EnemyAI enemyAI)
    {
        if (patrollingRoutine != null)
        {
            StopCoroutine(patrollingRoutine);
            patrollingRoutine = null;
        }
    }


    public override void Fighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        StopTheAction(enemyAI);

        enemyAI.EnemyStateMachine = enemyAI.EnemyStateFighting;
        enemyAI.EnemyStateMachine.Fighting(enemyAI, focusTarget);
    }


    public override void SeekingBattle(EnemyAI enemyAI) //Хочу потом переопределять этот метод, в зависимости от типа Enemy
    {
        if (patrollingRoutine == null)
        {
            patrollingRoutine = StartCoroutine(PatrollingEnumerator(enemyAI));
        }
    }


    private IEnumerator PatrollingEnumerator(EnemyAI enemyAI)
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
