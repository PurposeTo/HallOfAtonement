using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIStateMachine : MonoBehaviour
{

    // Патрулирует. Позже - (в зависимости от Monster/Guardian). Сейчас логика внутри класса-состояния.
    public abstract void Patrolling(EnemyAI enemyAI); 

    // Преследует и атакует. Позже - (в зависимости от типа боя). Сейчас логика внутри класса-состояния. 
    // Сейчас НЕ зависит от типа боя
    public abstract void Fighting(EnemyAI enemyAI, GameObject focusTarget); 
}
