using UnityEngine;

public abstract class PlayerControllerStateMachine : MonoBehaviour
{
    public abstract void Patrolling(PlayerController playerController); 


    public abstract void Fighting(PlayerController playerController); 
}
