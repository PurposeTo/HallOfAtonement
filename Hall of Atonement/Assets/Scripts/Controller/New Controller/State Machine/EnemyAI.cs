using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
//[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(EnemyAIPatrolling))]
[RequireComponent(typeof(EnemyAIHunting))]
public class EnemyAI : CharacterController
{
    private GameObject focusTarget;

    public EnemyStats MyEnemyStats { get; private protected set; }
    public IEnemyMode EnemyMode { get; private set; } // Ищет цель в зависимости от Monster/Guardian
    public EnemyAIStateMachine EnemyAIStateMachine { get; set; }
    public EnemyAIPatrolling EnemyAIPatrolling { get; private set; }
    public EnemyAIHunting EnemyAIHunting { get; private set; }


    private protected override void Start()
    {
        base.Start();
        MyEnemyStats = (EnemyStats)MyStats;

        Initialization();
    }


    private void Initialization()
    {
        EnemyMode = gameObject.GetComponent<IEnemyMode>();
        EnemyAIPatrolling = GetComponent<EnemyAIPatrolling>();
        EnemyAIHunting = GetComponent<EnemyAIHunting>();

        EnemyAIStateMachine = EnemyAIPatrolling;
        EnemyAIStateMachine.Patrolling(this);
    }


    public void DecideWhatToDo()
    {

        focusTarget = EnemyMode.SearchingTarget(MyEnemyStats.ViewingRadius);

        if (focusTarget == null) { EnemyAIStateMachine.Patrolling(this); }
        else { EnemyAIStateMachine.Hunting(this, focusTarget); }
    }
}
