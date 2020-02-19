using UnityEngine;

public class MeleeCombat : MonoBehaviour, IMelee
{
    public Transform attackPoint;
    public float attackRadius = .8f;


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


    public void Attack(CharacterCombat combat)
    {
        print(gameObject.name + " использует ближнюю атаку!");

        Collider2D[] hitUnits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);

        for (int i = 0; i < hitUnits.Length; i++)
        {
            if (hitUnits[i].gameObject != gameObject && hitUnits[i].TryGetComponent(out UnitStats targetStats))
            {
                combat.DoDamage(targetStats);
            }
        }
    }
}
