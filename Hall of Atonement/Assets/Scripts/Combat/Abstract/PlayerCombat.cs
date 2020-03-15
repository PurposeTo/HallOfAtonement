using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerCombat : CharacterCombat
{
    public void GetPlayerFightingLogic()
    {
        PreAttack(targetToAttack);
    }
}
