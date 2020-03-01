using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHunting : EnemyAIStateMachine
{
    private float timer = 3f;

    private Coroutine huntingRoutine;


    public override void Hunting(EnemyAITest enemyAI, GameObject focusTarget)
    {

        huntingRoutine = StartCoroutine(HuntingEnumerator(focusTarget));

        //Когда закончим, вызвать событие, говорящее о том, что мы закончили

    }


    public override void Patrolling(EnemyAITest enemyAI)
    {
        enemyAI.EnemyAIStateMachine = enemyAI.EnemyAIPatrolling;
    }


    private IEnumerator HuntingEnumerator(GameObject focusTarget) //Хочу потом переопределять этот метод, в зависимости от типа атаки
    {
        float timerCounter = timer;

        while (timerCounter > 0f
            && focusTarget != null 
            && (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= ViewingRadius))
        {
            //Значит все хорошо и можно продолжить охотиться





            yield return null;
            timerCounter -= Time.deltaTime;
        }

        huntingRoutine = null;
    }
}
