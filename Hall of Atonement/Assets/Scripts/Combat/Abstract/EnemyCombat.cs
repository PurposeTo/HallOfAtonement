using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombat : CharacterCombat
{
    private protected virtual void AttackTheTarget(GameObject target)
    {
        targetToAttack = target;
        PreAttack();
    }


    private protected abstract Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget);
}
