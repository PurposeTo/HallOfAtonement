using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyAIPatrolling))]
[RequireComponent(typeof(EnemyAIFighting))]
public class EnemyAI : CharacterController
{
    private GameObject focusTarget;

    public EnemyStats MyEnemyStats { get; private protected set; }
    public IEnemyType EnemyType { get; private set; } // Ищет цель в зависимости от Monster/Guardian
    public EnemyAIStateMachine EnemyAIStateMachine { get; set; }
    public EnemyAIPatrolling EnemyAIPatrolling { get; private set; }
    public EnemyAIFighting EnemyAIFighting { get; private set; }


    private protected override void Start()
    {
        base.Start();
        MyEnemyStats = (EnemyStats)MyStats;

        Initialization();
    }


    private void Initialization()
    {
        EnemyType = gameObject.GetComponent<IEnemyType>();
        EnemyAIPatrolling = GetComponent<EnemyAIPatrolling>();
        EnemyAIFighting = GetComponent<EnemyAIFighting>();

        EnemyAIStateMachine = EnemyAIPatrolling;
        EnemyAIStateMachine.Patrolling(this);
    }


    public void DecideWhatToDo()
    {

        focusTarget = EnemyType.SearchingTarget(MyEnemyStats.ViewingRadius);

        if (focusTarget == null) { EnemyAIStateMachine.Patrolling(this); }
        else { EnemyAIStateMachine.Fighting(this, focusTarget); }
    }
}
