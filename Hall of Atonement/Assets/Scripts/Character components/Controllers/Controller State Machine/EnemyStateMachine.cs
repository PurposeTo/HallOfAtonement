using UnityEngine;

public abstract class EnemyStateMachine : MonoBehaviour
{
    public abstract void SeekingBattle(EnemyAI enemyAI);


    public abstract void Fighting(EnemyAI enemyAI, GameObject focusTarget);

    public abstract Vector2 GetInputVector();

    private protected abstract void StopTheAction(EnemyAI enemyAI);
}
