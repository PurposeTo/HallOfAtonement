﻿using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : CharacterController
{
    private bool isWantToAttack = false;

    public Joystick joystick;

    private readonly RuntimePlatform currentRuntimePlatform = Application.platform;


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
