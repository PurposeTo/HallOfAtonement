using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombat : CharacterCombat
{
    private protected abstract Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget);
}
