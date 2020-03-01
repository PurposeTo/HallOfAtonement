using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyAIStateMachine))]
[RequireComponent(typeof(EnemyAIPatrolling))]
[RequireComponent(typeof(EnemyAIHunting))]

public class EnemyAITest : MonoBehaviour
{
    private GameObject focusTarget;

    private EnemyAIStateMachine EnemyAIStateMachine { get; set; }

    private EnemyAIPatrolling enemyAIPatrolling;
    private EnemyAIHunting enemyAIHunting;

    private void Start()
    {
        Initialization();
        DecideWhatToDo();
    }


    private void Initialization()
    {
        enemyAIPatrolling = GetComponent<EnemyAIPatrolling>();
        enemyAIHunting = GetComponent<EnemyAIHunting>();

        EnemyAIStateMachine = enemyAIPatrolling;
    }


    private void DecideWhatToDo()
    {
        focusTarget = EnemyAIStateMachine.SearchingTarget();

        if (focusTarget == null) { EnemyAIStateMachine = enemyAIPatrolling; }
        else { EnemyAIStateMachine = enemyAIHunting; }

        EnemyAIStateMachine.DoAction(focusTarget);

        //Do Action - после окончания всех своих дел вызовет событие, на которое мы подпишемся. 
        //В событии будет указанно, что нам необходимо опять решить, что нужно делать
    }
}
