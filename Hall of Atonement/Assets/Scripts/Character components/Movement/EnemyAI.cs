using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EnemyStatePatrolling))]
[RequireComponent(typeof(EnemyStateFighting))]
public class EnemyAI : CharacterMovement
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }
    public EnemyStateMachine EnemyStateMachine { get; set; }
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

        EnemyStateMachine = EnemyStatePatrolling;
        EnemyStateMachine.SeekingBattle(this);
    }


    public void DecideWhatToDo()
    {
        GameObject focusTarget = CharacterPresenter.CharacterType.SearchingTarget();

        if (focusTarget == null) { EnemyStateMachine.SeekingBattle(this); }
        else { EnemyStateMachine.Fighting(this, focusTarget); }
    }
}
