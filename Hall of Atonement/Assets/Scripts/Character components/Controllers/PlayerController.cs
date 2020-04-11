using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private readonly RuntimePlatform currentRuntimePlatform = Application.platform;

    private Joystick joystick;
    private AttackButtonEvent attackButton;
    public CharacterPresenter CharacterPresenter { get; private protected set; }
    public PlayerPresenter PlayerPresenter { get; private protected set; }

    public PlayerStateMachine PlayerStateMachine { get; set; }
    public PlayerStatePatrolling PlayerStatePatrolling { get; private set; }
    public PlayerStateFighting PlayerStateFighting { get; private set; }


    private void Start()
    {
        CharacterPresenter = gameObject.GetComponent<CharacterPresenter>();
        PlayerPresenter = (PlayerPresenter)CharacterPresenter;
        joystick = PlayerPresenter.PlayerUIPresenter.PlayerJoystick;
        attackButton = PlayerPresenter.PlayerUIPresenter.PlayerAttackButton;
        attackButton.OnPressingAttackButton += IsAttacking;


        PlayerStatePatrolling = GetComponent<PlayerStatePatrolling>();
        PlayerStateFighting = GetComponent<PlayerStateFighting>();

        PlayerStateMachine = PlayerStatePatrolling;
        PlayerStateMachine.Patrolling(this);
    }


    private void OnDestroy()
    {
        attackButton.OnPressingAttackButton -= IsAttacking;
    }


    private protected virtual void Update()
    {
        if (currentRuntimePlatform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                IsAttacking(true);
            }
            else if (Input.GetKeyUp(KeyCode.Space))
            {
                IsAttacking(false);
            }
        }
    }


    private protected virtual void FixedUpdate()
    {
        CharacterPresenter.CharacterMovement.MoveCharacter(GetInputVector());
    }


    public void IsAttacking(bool IsAttacking)
    {
        if (IsAttacking)
        {
            PlayerStateMachine.Fighting(this);
        }
        else
        {
            PlayerStateMachine.Patrolling(this);
        }
    }


    private Vector2 GetInputVector()
    {
        float horizontalInput;
        float verticalInput;
        Vector2 inputVector;

        switch (currentRuntimePlatform)
        {
            case RuntimePlatform.Android:
                horizontalInput = joystick.Horizontal;
                verticalInput = joystick.Vertical;

                if (Mathf.Abs(horizontalInput) < 0.2f)
                {
                    horizontalInput = 0f;
                }

                if (Mathf.Abs(verticalInput) < 0.2f)
                {
                    verticalInput = 0f;
                }
                break;

            case RuntimePlatform.WindowsEditor:
                horizontalInput = Input.GetAxis("Horizontal");
                verticalInput = Input.GetAxis("Vertical");
                break;
            default:
                horizontalInput = 0;
                verticalInput = 0;
                Debug.LogError("Unknown platform!");
                break;
        }

        inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        return inputVector;
    }
}
