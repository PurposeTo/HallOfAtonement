using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    public virtual float AttackRange { get; } = 6f;

    private protected override void Start()
    {
        base.Start();
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
