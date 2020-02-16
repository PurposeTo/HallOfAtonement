﻿using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : CharacterController
{
    private bool isOnAndroidMobile; //Игра запущена на андроиде?

    public Joystick joystick;

    private protected override void Start()
    {
        isOnAndroidMobile = RuntimePlatformDefinition();

        base.Start();

        myStats = (PlayerStats)myStats;
        combat = (PlayerCombat)combat;
    }


    private void Update()
    {
        inputVector = GetInputVector();
    }


    private Vector2 GetInputVector()
    {
        float horizontalInput;
        float verticalInput;
        Vector2 inputVector;

        if (isOnAndroidMobile)
        {
            if (joystick.Horizontal >= Mathf.Abs(0.2f))
            {
                horizontalInput = joystick.Horizontal;
            }
            else
            {
                horizontalInput = 0f;
            }

            if (joystick.Vertical >= Mathf.Abs(0.2f))
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
