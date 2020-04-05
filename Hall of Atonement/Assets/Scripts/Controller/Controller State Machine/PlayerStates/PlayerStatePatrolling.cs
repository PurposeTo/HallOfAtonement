using UnityEngine;

public class PlayerStatePatrolling : PlayerControllerStateMachine
{
    private Coroutine patrollingRoutine;


    public override void Fighting(PlayerController controller)
    {
        if (patrollingRoutine != null)
        {
            StopCoroutine(patrollingRoutine);
            patrollingRoutine = null;
        }

        controller.PlayerControllerStateMachine = controller.PlayerStateFighting;
        controller.PlayerControllerStateMachine.Fighting(controller);
    }

    public override void Patrolling(PlayerController controller)
    {
        //Тут ничего не должно происходить
    }
}
