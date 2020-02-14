using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public abstract class EnemyCombat : CharacterCombat
{
    public virtual float AttackRange { get; } = 5f;


    private protected override void Start()
    {
        base.Start();

        myStats = (EnemyStats)myStats;
        controller = (EnemyAI)controller;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }


    public override void SearchingTargetToAttack(GameObject target)
    {

        if (Vector2.Distance(target.transform.position, transform.position) <= AttackRange)
        {
            targetToAttack = target;
            PreAttack();

        }
        else
        {
            targetToAttack = null;
        }
    }
}
