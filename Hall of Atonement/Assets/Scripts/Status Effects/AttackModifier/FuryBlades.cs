using UnityEngine;

class FuryBlades : MonoBehaviour, IAttackModifier
{
    private CharacterPresenter characterPresenter;
    private const float upperBound = 0.9f;
    private const float modifierIncrease = 0.01f;
    private const float healthIncreaseValue = 0.01f;

    private CharacteristicModifier<float> attackSpeedModifier = new CharacteristicModifier<float>(0.1f);
    private CharacteristicModifier<float> criticalChanceModifier = new CharacteristicModifier<float>(0.1f);
    private CharacteristicModifier<float> criticalPowerModifier = new CharacteristicModifier<float>(0.1f);


    void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();
        characterPresenter.Combat.attackModifiers.Add(this);

        characterPresenter.MyStats.attackSpeed.AddModifier(attackSpeedModifier);
        characterPresenter.MyStats.criticalChance.AddModifier(criticalChanceModifier);
        characterPresenter.MyStats.criticalMultiplier.AddModifier(criticalPowerModifier);

        // Модификаторы вычисляются от базового значения статов
        characterPresenter.MyStats.attackSpeed.OnChangeStatBaseValue += SetAttackSpeedModifierValue;
        characterPresenter.MyStats.criticalChance.OnChangeStatBaseValue += SetCriticalChanceModifierValue;
        characterPresenter.MyStats.criticalMultiplier.OnChangeStatBaseValue += SetCriticalMultiplierModifierValue;
        characterPresenter.MyStats.OnChangedCurrentHealth += SetAllAttackModifiersValue;
    }


    void OnDestroy()
    {
        characterPresenter.Combat.attackModifiers.Remove(this);

        characterPresenter.MyStats.attackSpeed.RemoveModifier(attackSpeedModifier);
        characterPresenter.MyStats.criticalChance.RemoveModifier(criticalChanceModifier);
        characterPresenter.MyStats.criticalMultiplier.RemoveModifier(criticalPowerModifier);

        characterPresenter.MyStats.attackSpeed.OnChangeStatBaseValue -= SetAttackSpeedModifierValue;
        characterPresenter.MyStats.criticalChance.OnChangeStatBaseValue -= SetCriticalChanceModifierValue;
        characterPresenter.MyStats.criticalMultiplier.OnChangeStatBaseValue -= SetCriticalMultiplierModifierValue;
        characterPresenter.MyStats.OnChangedCurrentHealth -= SetAllAttackModifiersValue;
    }


    // Нужно делать активные и пассивные модификаторы атаки, ведь конкретно этот должен вызываться каждый кадр, а не только при атаке
    public void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery, bool isCritical = false)
    {
        if (isCritical)
        {
            characterPresenter.MyStats.Healing(damage * healthIncreaseValue);
        }
    }


    private void SetAllAttackModifiersValue()
    {
        if (characterPresenter.MyStats.CurrentHealthPoint < characterPresenter.MyStats.maxHealthPoint.GetValue() * upperBound)
        {
            // Изменять значение напрямую в зависимости от разницы здоровья в разные моменты времени (без прибавления или уменьшения)
            attackSpeedModifier.SetModifierValue(attackSpeedModifier.GetModifierValue() + modifierIncrease);
            criticalChanceModifier.SetModifierValue(criticalChanceModifier.GetModifierValue() + modifierIncrease);
            criticalPowerModifier.SetModifierValue(criticalPowerModifier.GetModifierValue() + modifierIncrease);
        }

    }


    private void SetAttackSpeedModifierValue()
    {
        float newValue = characterPresenter.MyStats.attackSpeed.GetBaseValue() * (attackSpeedModifier.GetModifierValue() + modifierIncrease);
        attackSpeedModifier.SetModifierValue(newValue);
    }


    private void SetCriticalChanceModifierValue()
    {
        float newValue = characterPresenter.MyStats.criticalChance.GetBaseValue() * (criticalChanceModifier.GetModifierValue() + modifierIncrease);
        criticalChanceModifier.SetModifierValue(newValue);
    }


    private void SetCriticalMultiplierModifierValue()
    {
        float newValue = characterPresenter.MyStats.criticalMultiplier.GetBaseValue() * (criticalPowerModifier.GetModifierValue() + modifierIncrease);
        criticalPowerModifier.SetModifierValue(newValue);
    }


    public object Clone()
    {
        return MemberwiseClone();
    }
}
