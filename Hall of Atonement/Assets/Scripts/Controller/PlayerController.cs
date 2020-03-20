using UnityEngine;

//[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerStats))]
[RequireComponent(typeof(PlayerStatePatrolling))]
[RequireComponent(typeof(PlayerStateFighting))]
public class PlayerController : CharacterController
{
    private Joystick joystick;
    private AttackButtonEvent attackButton;

    private readonly RuntimePlatform currentRuntimePlatform = Application.platform;

    public PlayerControllerStateMachine PlayerControllerStateMachine { get; set; }
    public PlayerStatePatrolling PlayerStatePatrolling { get; private set; }
    public PlayerStateFighting PlayerStateFighting { get; private set; }


    private protected override void Start()
    {
        base.Start();

        joystick = GameManager.instance.PlayerJoystick;
        attackButton = GameManager.instance.PlayerAttackButton;
        attackButton.OnPressingAttackButton += IsAttacking;


        PlayerStatePatrolling = GetComponent<PlayerStatePatrolling>();
        PlayerStateFighting = GetComponent<PlayerStateFighting>();

        PlayerControllerStateMachine = PlayerStatePatrolling;
        PlayerControllerStateMachine.Patrolling(this);
    }


    private void OnDestroy()
    {
        attackButton.OnPressingAttackButton -= IsAttacking;
    }


    private protected virtual void Update()
    {
        InputVector = GetInputVector();


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


    public void IsAttacking(bool IsAttacking)
    {
        if (IsAttacking)
        {
            PlayerControllerStateMachine.Fighting(this);
        }
        else
        {
            PlayerControllerStateMachine.Patrolling(this);
            CharacterPresenter.Combat.targetToAttack = null;
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
