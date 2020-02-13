using UnityEngine;

[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : CharacterController
{

    private protected override void Start()
    {
        base.Start();

        myStats = (PlayerStats)myStats;
        combat = (PlayerCombat)combat;
    }


    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        inputVector = new Vector2(horizontalInput, verticalInput);
        inputVector = Vector2.ClampMagnitude(inputVector, 1);
    }

}
