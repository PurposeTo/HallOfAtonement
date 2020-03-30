using System;
using UnityEngine;

public class Lifesteal : StatusEffect, IAttackModifier
{
    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Lifesteal;

    Sprite IStatusEffectLogic.StatusEffectSprite => GameManager.instance.GetStatusEffectData(StatusEffectType).StatusEffectSprite;

    private CharacterPresenter characterPresenter;
    private const float baseLifestealValue = 0.15f;

    private const int masteryPointsForBleeding = 5;
    private const float increaseForMastery = 0.01f;

    private void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();
        characterPresenter.AddStatusEffect(this);
    }


    private void OnDestroy()
    {
        characterPresenter.RemoveStatusEffect(this);
    }


    public void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery, bool isCritical = false)
    {
        if (!Mathf.Approximately(targetStats.bleedingResistance.GetValue(), 1f))
        {
            characterPresenter.MyStats.ExtraHealing(damage * (1f - targetStats.bleedingResistance.GetValue()) * (baseLifestealValue + (mastery * increaseForMastery))); // Lifesteal снижается сопротивлением к кровотечению
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


    object ICloneable.Clone()
    {
        return MemberwiseClone();
    }
}
