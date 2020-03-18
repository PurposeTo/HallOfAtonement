using UnityEngine;

public abstract class EnemyAIStateMachine : MonoBehaviour
{
    public abstract void SeekingBattle(EnemyAI enemyAI);


    public abstract void Fighting(EnemyAI enemyAI, GameObject focusTarget);


    private protected abstract void StopTheAction(EnemyAI enemyAI);
}
