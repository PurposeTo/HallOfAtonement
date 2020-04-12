using UnityEngine;

public abstract class CharacterMovementStateMachine : MonoBehaviour
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }

    private protected virtual void Start()
    {
        CharacterPresenter = GetComponent<CharacterPresenter>();
    }

    public abstract void MoveCharacter(Vector2 inputVector);

    public abstract bool TurnFaceToTarget(GameObject focusTarget, float rotationSpeed, float faceEuler);


    public abstract void EnableMovement(CharacterPresenter characterPresenter);


    public abstract void DisableMovement(CharacterPresenter characterPresenter);
}
