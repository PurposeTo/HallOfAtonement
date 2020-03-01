using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyAIStateMachine))]
[RequireComponent(typeof(EnemyAIPatrolling))]
[RequireComponent(typeof(EnemyAIHunting))]

public class EnemyAITest : MonoBehaviour
{
    private GameObject focusTarget;

    public EnemyAIStateMachine EnemyAIStateMachine { get; set; }

    public EnemyAIPatrolling EnemyAIPatrolling { get; private set; }
    public EnemyAIHunting EnemyAIHunting { get; private set; }

    private void Start()
    {
        Initialization();
        DecideWhatToDo();
    }


    private void Initialization()
    {
        EnemyAIPatrolling = GetComponent<EnemyAIPatrolling>();
        EnemyAIHunting = GetComponent<EnemyAIHunting>();

        EnemyAIStateMachine = EnemyAIPatrolling;
    }


    private void DecideWhatToDo()
    {
        focusTarget = EnemyAIStateMachine.SearchingTarget();

        if (focusTarget == null) { EnemyAIStateMachine.Patrolling(this); }
        else { EnemyAIStateMachine.Hunting(this, focusTarget); }

        //Класс-состояние после окончания всех своих дел вызовет событие, на которое мы подпишемся. 
        //В событии будет указанно, что нам необходимо опять решить, что нужно делать
    }
}
