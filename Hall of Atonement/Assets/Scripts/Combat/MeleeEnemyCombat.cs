using UnityEngine;


[RequireComponent(typeof(MeleeCombat))]
public class MeleeEnemyCombat : EnemyCombat
{
    public override float AttackRange { get; } = 3f;

    private protected override IAttacker Attacker { get; set; }

    private protected override void Start()
    {
        base.Start();

        Attacker = gameObject.GetComponent<MeleeCombat>();
    }

}
