﻿using UnityEngine;

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


    private protected override Vector2 GetInputVector()
    {
        return EnemyAIStateMachine.GetInputVector(this);
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
        GameObject focusTarget = CharacterPresenter.CharacterType.SearchingTarget();

        if (focusTarget == null) { EnemyAIStateMachine.SeekingBattle(this); }
        else { EnemyAIStateMachine.Fighting(this, focusTarget); }
    }
}

