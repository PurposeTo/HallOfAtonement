using UnityEngine;

public class PlayerStatePatrolling : PlayerStateMachine
{
    private Coroutine patrollingRoutine;


    public override void Fighting(PlayerMovement movement)
    {
        StopTheAction(movement);

        movement.PlayerStateMachine = movement.PlayerStateFighting;
        movement.PlayerStateMachine.Fighting(movement);
    }

    public override void Patrolling(PlayerMovement movement)
    {
        // Тут ничего не должно происходить
    }

    private protected override void StopTheAction(PlayerMovement playerMovement)
    {
        if (patrollingRoutine != null)
        {
            StopCoroutine(patrollingRoutine);
            patrollingRoutine = null;
        }
    }
}
