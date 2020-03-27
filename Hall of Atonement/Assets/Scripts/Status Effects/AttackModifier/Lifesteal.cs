﻿using UnityEngine;

public class Lifesteal : MonoBehaviour, IAttackModifier
{
    private CharacterPresenter characterPresenter;
    private const float baseLifestealValue = 0.15f;

    private const int masteryPointsForBleeding = 5;
    private const float increaseForMastery = 0.01f;


    private void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();
        characterPresenter.Combat.attackModifiers.Add(this);
    }


    private void OnDestroy()
    {
        characterPresenter.Combat.attackModifiers.Remove(this);
    }


    public void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery, bool isCritical = false)
    {
        if (!Mathf.Approximately(targetStats.bleedingResistance.GetValue(), 1f))
        {
            characterPresenter.MyStats.Healing(damage * (1f - targetStats.bleedingResistance.GetValue()) * (baseLifestealValue + (mastery * increaseForMastery))); // Lifesteal снижается сопротивлением к кровотечению
            BleedingFromLifesteal(targetStats, mastery, damage);

            Debug.Log(gameObject.name + ": \"Your life is mine!\"");
        }
    }


    private void BleedingFromLifesteal(UnitStats targetStats, int mastery, float damage)
    {
        if (mastery >= 15)
        {
            new StatusEffectFactory<Bleeding>(targetStats.gameObject, characterPresenter.MyStats, mastery / masteryPointsForBleeding);
        }
    }


    public object Clone()
    {
        return MemberwiseClone();
    }
}
