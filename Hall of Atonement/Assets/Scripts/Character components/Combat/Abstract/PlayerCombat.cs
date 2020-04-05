using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public abstract class PlayerCombat : CharacterCombat
{
    public void GetPlayerFightingLogic()
    {
        PreAttack(targetToAttack);
    }
}
