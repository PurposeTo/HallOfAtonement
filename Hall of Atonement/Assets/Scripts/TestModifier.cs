using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestModifier : MonoBehaviour
{
    float BasefrozenPercent = 0.6f;


    private CharacteristicModifier<float> modifierMovementSpeed = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> modifierRotationSpeed = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> modifierAttackSpeed = new CharacteristicModifier<float>();

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
