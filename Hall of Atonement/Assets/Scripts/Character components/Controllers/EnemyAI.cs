using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }
    public EnemyPresenter EnemyPresenter { get; private protected set; }
    public EnemyStateMachine EnemyStateMachine { get; set; }
    public EnemyStatePatrolling EnemyStatePatrolling { get; private set; }
    public EnemyStateFighting EnemyStateFighting { get; private set; }


    private void Start()
    {
        CharacterPresenter = gameObject.GetComponent<CharacterPresenter>();

        EnemyPresenter = (EnemyPresenter)CharacterPresenter;

        Initialization();
    }


    private protected virtual void FixedUpdate()
    {
        CharacterPresenter.CharacterMovement.MoveCharacter(GetInputVector());
    }


    private Vector2 GetInputVector()
    {
        return EnemyStateMachine.GetInputVector();
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

