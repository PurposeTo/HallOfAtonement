using UnityEngine;

public abstract class PlayerControllerStateMachine : MonoBehaviour
{
    public abstract void Patrolling(PlayerController playerController); 


    public abstract void Fighting(PlayerController playerController);


    private protected abstract void StopTheAction(PlayerController playerController);
}
