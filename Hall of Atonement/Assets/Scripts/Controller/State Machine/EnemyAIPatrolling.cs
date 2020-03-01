using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIPatrolling : EnemyAIStateMachine
{
    public override void DoAction(GameObject focusTarget)
    {
        Patrolling();

        //Вызвать событие, говорящее о том, что мы закончили
    }

    private void Patrolling() //Хочу потом переопределять этот метод, в зависимости от типа Enemy
    {
        //Патрулировать
    }
}
