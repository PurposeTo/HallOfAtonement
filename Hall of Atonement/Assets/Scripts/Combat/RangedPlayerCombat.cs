using UnityEngine;

[RequireComponent(typeof(RangedCombat))]
public class RangedPlayerCombat : PlayerCombat
{
    private protected override IAttacker Attacker { get; set; }
    
    private protected override void Start()
    {
        base.Start();

        Attacker = gameObject.GetComponent<RangedCombat>();
    }
}
