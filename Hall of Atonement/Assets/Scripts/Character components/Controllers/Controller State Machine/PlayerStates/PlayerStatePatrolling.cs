using UnityEngine;

public class PlayerStatePatrolling : PlayerControllerStateMachine
{
    private Coroutine patrollingRoutine;


    public override void Fighting(PlayerController playerController)
    {
        StopTheAction(playerController);

        playerController.PlayerControllerStateMachine = playerController.PlayerStateFighting;
        playerController.PlayerControllerStateMachine.Fighting(playerController);
    }

    public override void Patrolling(PlayerController playerController)
    {
        // Тут ничего не должно происходить
    }

    private protected override void StopTheAction(PlayerController playerController)
    {
        if (patrollingRoutine != null)
        {
            StopCoroutine(patrollingRoutine);
            patrollingRoutine = null;
        }
    }
}
