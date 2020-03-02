using UnityEngine;


[RequireComponent(typeof(MeleeCombat))]
public class MeleeEnemyLogic : EnemyCombat
{
    // Здесь должен быть метод, который говорит, как надо драться врагу с ближним типом боя

    public override float AttackRange { get; } = 1.2f;

}
