using UnityEngine;
public abstract class CharacterController : MonoBehaviour
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }


    private protected virtual void Start()
    {
        CharacterPresenter = gameObject.GetComponent<CharacterPresenter>();
    }


    private protected virtual void FixedUpdate()
    {
        CharacterPresenter.CharacterMovement.MoveCharacter(GetInputVector());
    }


    private protected abstract Vector2 GetInputVector();
}
