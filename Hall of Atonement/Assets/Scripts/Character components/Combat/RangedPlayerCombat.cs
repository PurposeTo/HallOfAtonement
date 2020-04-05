using UnityEngine;

[RequireComponent(typeof(RangedLaserWeapon))]
public class RangedPlayerCombat : PlayerCombat
{
    //Этот класс необходим ради [RequireComponent(typeof(RangedCombat))]
}
