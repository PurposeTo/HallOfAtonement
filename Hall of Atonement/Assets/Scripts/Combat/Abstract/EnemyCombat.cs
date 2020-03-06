using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombat : CharacterCombat, IEnemyAttackType
{
    private protected abstract Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget);


    void IEnemyAttackType.GetEnemyFightingLogic(EnemyAI enemyAI, GameObject focusTarget)
    {
        enemyAI.InputVector = GetMovingVectorOnFighting(enemyAI, focusTarget);

        // Как/когда нужно атаковать?
        PreAttack(focusTarget);
    }
}
