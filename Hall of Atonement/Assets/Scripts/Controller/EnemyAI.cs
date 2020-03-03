using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
//[RequireComponent(typeof(EnemyCombat))]
[RequireComponent(typeof(EnemyAIPatrolling))]
[RequireComponent(typeof(EnemyAIFighting))]
public class EnemyAI : CharacterController
{
    private GameObject focusTarget;

    public EnemyStats MyEnemyStats { get; private protected set; }
    public IEnemyType EnemyType { get; private set; } // Ищет цель в зависимости от Monster/Guardian
    public EnemyAIStateMachine EnemyAIStateMachine { get; set; }
    public EnemyAIPatrolling EnemyAIPatrolling { get; private set; }
    public EnemyAIFighting EnemyAIHunting { get; private set; }


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
        EnemyAIHunting = GetComponent<EnemyAIFighting>();

        EnemyAIStateMachine = EnemyAIPatrolling;
        EnemyAIStateMachine.Patrolling(this);
    }


    public void DecideWhatToDo()
    {

        focusTarget = EnemyType.SearchingTarget(MyEnemyStats.ViewingRadius);
        Combat.targetToAttack = focusTarget; // Сейчас, если ты не в радиусе атаки но враги тебя видят, то они двигаются НЕ лицом к тебе

        if (focusTarget == null) { EnemyAIStateMachine.Patrolling(this); }
        else { EnemyAIStateMachine.Fighting(this, focusTarget); }
    }
}
