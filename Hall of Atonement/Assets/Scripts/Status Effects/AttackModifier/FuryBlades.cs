using UnityEngine;

class FuryBlades : StatusEffect, IAttackModifier
{
    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.FuryBlades;

    Sprite IStatusEffectLogic.StatusEffectSprite => GameManager.instance.GetStatusEffectData(StatusEffectType).StatusEffectSprite;

    private CharacterPresenter characterPresenter;
    private const float upperBound = 0.9f;
    private const float healthIncreaseValue = 0.01f;
    private const float maxBaseStatModifier = 0.6f;
    private const float minBaseStatModifier = 0f;
    private const float increaseForMastery = 0.01f;

    private CharacteristicModifier<float> attackSpeedModifier = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> criticalChanceModifier = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> criticalPowerModifier = new CharacteristicModifier<float>();


    void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();
        characterPresenter.AddStatusEffect(this);

        characterPresenter.MyStats.attackSpeed.AddModifier(attackSpeedModifier);
        characterPresenter.MyStats.criticalChance.AddModifier(criticalChanceModifier);
        characterPresenter.MyStats.criticalMultiplier.AddModifier(criticalPowerModifier);

        characterPresenter.MyStats.OnChangedCurrentHealth += SetAllAttackModifiersValue;
    }


    void OnDestroy()
    {
        characterPresenter.RemoveStatusEffect(this);

        characterPresenter.MyStats.attackSpeed.RemoveModifier(attackSpeedModifier);
        characterPresenter.MyStats.criticalChance.RemoveModifier(criticalChanceModifier);
        characterPresenter.MyStats.criticalMultiplier.RemoveModifier(criticalPowerModifier);

        characterPresenter.MyStats.OnChangedCurrentHealth -= SetAllAttackModifiersValue;
    }


    public void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery, bool isCritical = false)
    {
        if (isCritical)
        {
            characterPresenter.MyStats.ExtraHealing(damage * healthIncreaseValue);
        }
    }


    // Изменение характеристик
    private void SetAllAttackModifiersValue()
    {
        float currentHealthPercent = characterPresenter.MyStats.CurrentHealthPoint / characterPresenter.MyStats.maxHealthPoint.GetValue();

        float statModifierValue = Mathf.Lerp(maxBaseStatModifier + (characterPresenter.MyStats.mastery.GetValue() * increaseForMastery), minBaseStatModifier, currentHealthPercent / upperBound);
        attackSpeedModifier.SetModifierValue(statModifierValue);
        criticalChanceModifier.SetModifierValue(statModifierValue);
        criticalPowerModifier.SetModifierValue(statModifierValue);
    }


    public object Clone()
    {
        return MemberwiseClone();
    }
}
