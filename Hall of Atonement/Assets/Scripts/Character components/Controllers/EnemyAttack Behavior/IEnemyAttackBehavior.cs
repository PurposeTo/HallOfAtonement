using UnityEngine;

// Данный интерфейст нужен для того, что бы определять логику поведения врагов с определенным оружием
public interface IEnemyAttackBehavior 
{
    Vector2 GetMovingVectorOnFighting(GameObject focusTarget);

    float GetAttackRange();
}
