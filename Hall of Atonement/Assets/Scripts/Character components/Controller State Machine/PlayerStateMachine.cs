using UnityEngine;

public abstract class PlayerStateMachine : MonoBehaviour
{
    public abstract void Patrolling(PlayerMovement playerMovement); 


    public abstract void Fighting(PlayerMovement playerMovement);


    private protected abstract void StopTheAction(PlayerMovement playerMovement);
}
