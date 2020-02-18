using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : CharacterController
{
    private bool isWantToAttack = false;

    private bool isOnAndroidMobile; //Игра запущена на андроиде?

    public Joystick joystick;

    private protected override void Start()
    {
        isOnAndroidMobile = RuntimePlatformDefinition();

        base.Start();
    }


    private void Update()
    {
        inputVector = GetInputVector();

        if (Input.GetKey(KeyCode.Space)) //Если нажата кнопка атаки
        {
            isWantToAttack = true;
        }
        else //Если НЕ нажата кнопка атаки
        {
            isWantToAttack = false;
            combat.targetToAttack = null;
        }
    }


    private protected override void FixedUpdate()
    {
        if (isWantToAttack)
        {
            //Искать цель
            combat.SearchingTargetToAttack(combat.targetToAttack);
        }

        base.FixedUpdate();
    }


    private Vector2 GetInputVector()
    {
        float horizontalInput;
        float verticalInput;
        Vector2 inputVector;

        if (isOnAndroidMobile)
        {
            if (Mathf.Abs(joystick.Horizontal) >= 0.2f)
            {
                horizontalInput = joystick.Horizontal;
            }
            else
            {
                horizontalInput = 0f;
            }

            if (Mathf.Abs(joystick.Vertical) >= 0.2f)
            {
                verticalInput = joystick.Vertical;
            }
            else
            {
                verticalInput = 0f;
            }
        }
        else
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
        }


        inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);

        return inputVector;
    }


    private bool RuntimePlatformDefinition()
    {
        bool togle = false;
        //Позже bool заменить на множество переключателей

        if (Application.platform == RuntimePlatform.Android)
        {
            togle = true;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            togle = false;
        }

        return togle;
    }
}
