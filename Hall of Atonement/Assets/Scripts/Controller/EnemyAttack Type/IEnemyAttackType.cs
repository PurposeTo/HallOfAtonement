using System.Collections;
using UnityEngine;

public interface IEnemyAttackType
{
    // Логика, как стоит двигаться при атаке
    Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget);

    void AttackTheTarget(GameObject target);
}
