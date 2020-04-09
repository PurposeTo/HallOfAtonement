using UnityEngine;

public class EnemyPresenter : CharacterPresenter
{
    public EnemyStats MyEnemyStats { get; private protected set; }
    public EnemyCombat EnemyCombat { get; private protected set; }
    public EnemyAI EnemyAI { get; private protected set; }


    private protected override void Awake()
    {
        base.Awake();
        MyEnemyStats = (EnemyStats)MyStats;
        EnemyCombat = (EnemyCombat)Combat;
        EnemyAI = gameObject.GetComponent<EnemyAI>();
    }
}
