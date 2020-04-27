using UnityEngine;

public abstract class EnemyAIStateMachine : MonoBehaviour
{
    public abstract void SeekingBattle(EnemyAI enemyAI);

    public abstract void Fighting(EnemyAI enemyAI, GameObject focusTarget);

    public abstract Vector2 GetInputVector(EnemyAI enemyAI); // Вызывает метод из EnemyAttackType

    private protected abstract void StopTheAction(EnemyAI enemyAI);
}
