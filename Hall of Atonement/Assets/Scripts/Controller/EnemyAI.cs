using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyStatePatrolling))]
[RequireComponent(typeof(EnemyStateHunting))]
[RequireComponent(typeof(EnemyStateFighting))]
public class EnemyAI : CharacterController
{
    public GameObject FocusTarget { get; private set; } //Цель, на которой враг в данный момент сфокусирован

    public EnemyStats MyEnemyStats { get; private protected set; }
    public IEnemyType EnemyType { get; private set; } // Ищет цель в зависимости от Monster/Guardian
    public EnemyAIStateMachine EnemyAIStateMachine { get; set; }
    public EnemyStatePatrolling EnemyStatePatrolling { get; private set; }
    public EnemyStateHunting EnemyStateHunting { get; private set; }
    public EnemyStateFighting EnemyStateFighting { get; private set; }


    private protected override void Start()
    {
        base.Start();
        MyEnemyStats = (EnemyStats)myStats;

        Initialization();
    }


    private void Initialization()
    {
        EnemyType = gameObject.GetComponent<IEnemyType>();
        EnemyStatePatrolling = GetComponent<EnemyStatePatrolling>();
        EnemyStateHunting = GetComponent<EnemyStateHunting>();
        EnemyStateFighting = GetComponent<EnemyStateFighting>();

        EnemyAIStateMachine = EnemyStatePatrolling;
        EnemyAIStateMachine.SeekingBattle(this);
    }


    public void DecideWhatToDo()
    {
        FocusTarget = EnemyType.SearchingTarget(MyEnemyStats.ViewingRadius);

        if (FocusTarget == null) { EnemyAIStateMachine.SeekingBattle(this); }
        else { EnemyAIStateMachine.Fighting(this); }
    }
}
