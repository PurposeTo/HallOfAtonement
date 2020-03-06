using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyStatePatrolling))]
[RequireComponent(typeof(EnemyStateFighting))]
public class EnemyAI : CharacterController
{
    private GameObject focusTarget;

    public EnemyStats MyEnemyStats { get; private protected set; }
    public IEnemyType EnemyType { get; private set; } // Ищет цель в зависимости от Monster/Guardian
    public EnemyAIStateMachine EnemyAIStateMachine { get; set; }
    public EnemyStatePatrolling EnemyStatePatrolling { get; private set; }
    public EnemyStateFighting EnemyStateFighting { get; private set; }


    private protected override void Start()
    {
        base.Start();
        MyEnemyStats = (EnemyStats)MyStats;

        Initialization();
    }


    private void Initialization()
    {
        EnemyType = gameObject.GetComponent<IEnemyType>();
        EnemyStatePatrolling = GetComponent<EnemyStatePatrolling>();
        EnemyStateFighting = GetComponent<EnemyStateFighting>();

        EnemyAIStateMachine = EnemyStatePatrolling;
        EnemyAIStateMachine.Patrolling(this);
    }


    public void DecideWhatToDo()
    {

        focusTarget = EnemyType.SearchingTarget(MyEnemyStats.ViewingRadius);

        if (focusTarget == null) { EnemyAIStateMachine.Patrolling(this); }
        else { EnemyAIStateMachine.Fighting(this, focusTarget); }
    }
}
