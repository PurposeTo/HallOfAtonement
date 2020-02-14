using UnityEngine;


[RequireComponent(typeof(MeleeCombat))]
public class MeleeEnemyCombat : EnemyCombat
{
    public override float AttackRange { get; } = 3f;

    private protected override void Start()
    {
        base.Start();

        Attacker = gameObject.GetComponent<MeleeCombat>();
    }

}
