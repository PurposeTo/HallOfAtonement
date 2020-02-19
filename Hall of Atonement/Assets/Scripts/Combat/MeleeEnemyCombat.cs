using UnityEngine;


[RequireComponent(typeof(MeleeCombat))]
public class MeleeEnemyCombat : EnemyCombat
{
    public override float AttackRange { get; } = 1.2f;

    private protected override void Start()
    {
        base.Start();

        Attacker = gameObject.GetComponent<MeleeCombat>();
    }

}
