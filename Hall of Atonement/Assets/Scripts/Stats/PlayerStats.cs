using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : CharacterStats
{
    public override void GetExperience(int amount)
    {
        base.GetExperience(amount / 7);
    }
}