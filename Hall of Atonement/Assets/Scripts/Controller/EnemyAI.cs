﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyStatePatrolling))]
[RequireComponent(typeof(EnemyStateFighting))]
public class EnemyAI : CharacterController
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }
    public EnemyAIStateMachine EnemyAIStateMachine { get; set; }
    public EnemyStatePatrolling EnemyStatePatrolling { get; private set; }
    public EnemyStateFighting EnemyStateFighting { get; private set; }


    private protected override void Start()
    {
        base.Start();
        EnemyPresenter = (EnemyPresenter)CharacterPresenter;

        Initialization();
    }


    private void Initialization()
    {
        EnemyStatePatrolling = GetComponent<EnemyStatePatrolling>();
        EnemyStateFighting = GetComponent<EnemyStateFighting>();

        EnemyAIStateMachine = EnemyStatePatrolling;
        EnemyAIStateMachine.SeekingBattle(this);
    }


    public void DecideWhatToDo()
    {
        GameObject focusTarget = CharacterPresenter.CharacterType.SearchingTarget(EnemyPresenter.MyEnemyStats.ViewingRadius);

        if (focusTarget == null) { EnemyAIStateMachine.SeekingBattle(this); }
        else { EnemyAIStateMachine.Fighting(this, focusTarget); }
    }
}
