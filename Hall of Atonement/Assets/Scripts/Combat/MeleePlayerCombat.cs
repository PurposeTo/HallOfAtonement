using UnityEngine;

[RequireComponent(typeof(MeleeCombat))]
public class MeleePlayerCombat : PlayerCombat
{
    private protected override IAttacker Attacker { get; set; }


    private protected override void Start()
    {
        base.Start();

        Attacker = gameObject.GetComponent<MeleeCombat>();
    }
}
