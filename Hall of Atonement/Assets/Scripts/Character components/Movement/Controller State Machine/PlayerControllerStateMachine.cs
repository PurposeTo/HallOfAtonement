using UnityEngine;

public abstract class PlayerControllerStateMachine : MonoBehaviour
{
    public abstract void Patrolling(PlayerMovement playerController); 


    public abstract void Fighting(PlayerMovement playerController); 
}
