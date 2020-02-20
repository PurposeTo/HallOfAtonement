using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : CharacterStats
{
    public override void GetExperience(int amount, out bool isLvlUp)
    {
        base.GetExperience(amount / 7, out isLvlUp);
    }
}