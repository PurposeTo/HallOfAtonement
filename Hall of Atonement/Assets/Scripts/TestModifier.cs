using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModifier : MonoBehaviour, IStatModifier
{
    private float slowly = -5f; //Временный тест


    private void Update()
    {
        slowly -= 0.02f;
    }


    private void Start()
    {
        if (gameObject.TryGetComponent(out CharacterStats characterStats))
        {
            characterStats.movementSpeed.AddModifier(this); //Временный тест
        }
    }


    private void OnDisable()
    {
        if (gameObject.TryGetComponent(out CharacterStats characterStats))
        {
            characterStats.movementSpeed.RemoveModifier(this); //Временный тест
        }
    }


    float IStatModifier.GetModifierValue()
    {
        return slowly;
    }
}
