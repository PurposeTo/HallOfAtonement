using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAIStateMachine : MonoBehaviour
{
    private protected float ViewingRadius { get; set; } = 10f;

    public abstract void DoAction(GameObject focusTarget);

    public GameObject SearchingTarget() { return null; }
}
