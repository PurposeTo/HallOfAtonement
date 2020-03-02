using UnityEngine;

[RequireComponent(typeof(RangedCombat))]
public class RangedEnemyLogic : EnemyCombat
{
    // Здесь должен быть метод, который говорит, как надо драться врагу с ближним типом боя

    public override float AttackRange { get; } = 6f;
}
