using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIPatrolling : EnemyAIStateMachine
{
    public override void Hunting(EnemyAITest enemyAI, GameObject focusTarget)
    {
        enemyAI.EnemyAIStateMachine = enemyAI.EnemyAIHunting;
    }

    public override void Patrolling(EnemyAITest enemyAI) //Хочу потом переопределять этот метод, в зависимости от типа Enemy
    {
        //Патрулировать

        //После патрулирования вызвать событие и сказать, что мы закончили
    }
}
