using UnityEngine;

public abstract class PlayerCombat : CharacterCombat
{
    public void GetPlayerFightingLogic()
    {
        PreAttack(targetToAttack);
    }
}
