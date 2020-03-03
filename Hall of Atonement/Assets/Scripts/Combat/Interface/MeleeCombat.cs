using UnityEngine;

public class MeleeCombat : MonoBehaviour, IMelee
{
    public Transform attackPoint;

    public float MeleeAttackRange { get; set; } = .8f;


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, MeleeAttackRange);
    }


    void IAttacker.Attack(CharacterCombat combat)
    {
        print(gameObject.name + " использует ближнюю атаку!");

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(attackPoint.position, MeleeAttackRange);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            if (hitUnits[i].gameObject != gameObject && hitUnits[i].TryGetComponent(out UnitStats targetStats))
            {
                combat.DoDamage(targetStats);
            }
        }
    }
}
