using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModifier : MonoBehaviour
{
    float BasefrozenPercent = 0.6f;


    private ParameterModifier<float> modifierMovementSpeed = new ParameterModifier<float>();
    private ParameterModifier<float> modifierRotationSpeed = new ParameterModifier<float>();
    private ParameterModifier<float> modifierAttackSpeed = new ParameterModifier<float>();

    private UnitStats targetStats;


    private void Start()
    {
        targetStats = gameObject.GetComponent<UnitStats>();

        if (gameObject.TryGetComponent(out CharacterStats characterStats))
        {
            //modifierMovementSpeed.ModifierValue = characterStats.movementSpeed.GetBaseValue() * frozenPercent;
            //modifierRotationSpeed.ModifierValue = characterStats.rotationSpeed.GetBaseValue() * frozenPercent;
            //modifierAttackSpeed.ModifierValue = characterStats.attackSpeed.GetBaseValue() * frozenPercent;


            characterStats.movementSpeed.AddModifier(modifierMovementSpeed);
            characterStats.rotationSpeed.AddModifier(modifierRotationSpeed);
            characterStats.attackSpeed.AddModifier(modifierAttackSpeed);
        }
    }


    private void OnDisable()
    {
        if (gameObject.TryGetComponent(out CharacterStats characterStats))
        {
            characterStats.movementSpeed.RemoveModifier(modifierMovementSpeed);
            characterStats.rotationSpeed.RemoveModifier(modifierRotationSpeed);
            characterStats.attackSpeed.RemoveModifier(modifierAttackSpeed);
        }
    }
}
