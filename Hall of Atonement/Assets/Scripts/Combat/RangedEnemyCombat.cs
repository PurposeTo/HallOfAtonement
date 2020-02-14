using UnityEngine;

[RequireComponent(typeof(RangedCombat))]
public class RangedEnemyCombat : EnemyCombat
{
    public override float AttackRange { get; } = 5f;

    private protected override void Start()
    {
        base.Start();

        Attacker = gameObject.GetComponent<RangedCombat>();
    }
}
