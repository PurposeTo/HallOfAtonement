using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIHunting : EnemyAIStateMachine
{
    private float timer = 3f;

    private Coroutine doHuntingRoutine;


    public override void DoAction(GameObject focusTarget)
    {

        doHuntingRoutine = StartCoroutine(DoHunting(focusTarget));

        //Вызвать событие, говорящее о том, что мы закончили

    }


    private IEnumerator DoHunting(GameObject focusTarget) //Хочу потом переопределять этот метод, в зависимости от типа атаки
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

        doHuntingRoutine = null;
    }
}
