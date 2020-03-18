using UnityEngine;

public interface IEnemyAttackType
{
    // Логика, как стоит вести себя в драке с целью
    void GetEnemyFightingLogic(EnemyAI enemyAI, GameObject focusTarget);
}
