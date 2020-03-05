using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : CharacterController
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private AttackButtonEvent attackButton;

    private PlayerCombat playerCombat;

    private readonly RuntimePlatform currentRuntimePlatform = Application.platform;


    private protected override void Start()
    {
        base.Start();
        playerCombat = (PlayerCombat)Combat;
    }


    private void OnEnable()
    {
        attackButton.OnPressingAttackButton += IsAttacking;
    }


    private void OnDisable()
    {
        attackButton.OnPressingAttackButton += IsAttacking;
    }


    private protected virtual void Update()
    {
        InputVector = GetInputVector();

        //if (Input.GetKey(KeyCode.Space)) //Если нажата кнопка атаки
        //{
        //    playerCombat.SearchingTarget();
        //}
        //else //Если НЕ нажата кнопка атаки
        //{
        //    Combat.targetToAttack = null;
        //}

        IsAttacking(Input.GetKey(KeyCode.Space));
    }


    public void IsAttacking(bool IsAttacking)
    {
        if (IsAttacking)
        {
            playerCombat.SearchingTarget();
        }
        else
        {
            Combat.targetToAttack = null;
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
