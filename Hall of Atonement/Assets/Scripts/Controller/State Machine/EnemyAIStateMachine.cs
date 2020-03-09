using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIStateMachine : MonoBehaviour
{
    public abstract void SeekingBattle(EnemyAI enemyAI);


    public abstract void Fighting(EnemyAI enemyAI);


    public abstract void BeginTheHunt(EnemyAI enemyAI, GameObject huntingTarget);


    private protected abstract void StopTheAction(EnemyAI enemyAI);
}
