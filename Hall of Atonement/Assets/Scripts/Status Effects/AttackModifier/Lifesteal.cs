using System;
using UnityEngine;

public class Lifesteal : StatusEffect, IAttackModifier
{
    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Lifesteal;

    public override StatusEffectData StatusEffectData => StatusEffectDataContainer.Instance.GetStatusEffectData(StatusEffectType);

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
            float _healing = damage * (1f - targetStats.bleedingResistance.GetValue()) * (baseLifestealValue + (mastery * increaseForMastery));
            characterPresenter.MyStats.Healing(_healing, true); // Lifesteal снижается сопротивлением к кровотечению
            BleedingFromLifesteal(targetStats, mastery);

            Debug.Log(gameObject.name + ": \"Your life is mine!\"");
        }
    }


    private void BleedingFromLifesteal(UnitStats targetStats, int mastery)
    {
        if (mastery >= 15)
        {
            new StatusEffectFactory<Bleeding>(targetStats, characterPresenter.MyStats, mastery / masteryPointsForBleeding);
        }
    }


    object ICloneable.Clone()
    {
        return MemberwiseClone();
    }
}
