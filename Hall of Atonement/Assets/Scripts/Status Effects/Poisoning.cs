using UnityEngine;

class Poisoning : ActiveEffect, IDamageLogic
{
    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Poisoning;

    public override StatusEffectData StatusEffectData => GameManager.instance.GetStatusEffectData(StatusEffectType);

    private UnitPresenter unitPresenter;
    private DamageType damageType;

    private CharacterStats myStats;
    private CharacteristicModifier<int> strengthModifierForPoisoning = new CharacteristicModifier<int>();
    private CharacteristicModifier<int> agilityModifierForPoisoning = new CharacteristicModifier<int>();
    private CharacteristicModifier<int> masteryModifierForPoisoning = new CharacteristicModifier<int>();

    private protected override float BaseDurationTime => 5f;
    private readonly float baseDamagePerSecond = 0.3f;

    private const float attributeModifierPercent = 0.1f;
    private const float attributeModifierIncrease = 0.01f;
    private const float decrease = -1f; // от finalValue должно ОТНИМАТЬСЯ значение модификатора характеристик эффекта Poisoning


    private void Awake()
    {
        // Внимание! Инициализация должна быть строго в Awake, так как он вызывается до AmplifyEffect
        Initialization();

        unitPresenter.AddStatusEffect(this);

        if (unitPresenter.UnitStats is CharacterStats)
        {
            myStats = (CharacterStats)unitPresenter.UnitStats;
            SetAllModifiersValue();

            myStats.strength.AddModifier(strengthModifierForPoisoning);
            myStats.agility.AddModifier(agilityModifierForPoisoning);
            myStats.mastery.AddModifier(masteryModifierForPoisoning);

            // Значение модификатора характеристик эффекта Poisoning меняется в соответствии с базовым значением характеристик
            myStats.strength.OnChangeAttributeBaseValue += SetStrengthModifierValue;
            myStats.agility.OnChangeAttributeBaseValue += SetAgilityModifierValue;
            myStats.mastery.OnChangeAttributeBaseValue += SetMasteryModifierValue;
        }
    }


    private void OnDestroy()
    {
        unitPresenter.RemoveStatusEffect(this);

        if (myStats != null)
        {
            myStats.strength.RemoveModifier(strengthModifierForPoisoning);
            myStats.agility.RemoveModifier(agilityModifierForPoisoning);
            myStats.mastery.RemoveModifier(masteryModifierForPoisoning);

            myStats.strength.OnChangeAttributeBaseValue -= SetStrengthModifierValue;
            myStats.agility.OnChangeAttributeBaseValue -= SetAgilityModifierValue;
            myStats.mastery.OnChangeAttributeBaseValue -= SetMasteryModifierValue;
        }
    }


    private void Update()
    {
        float currentDurationTime = GetCurrentDurationTime();
        if (currentDurationTime > 0f)
        {
            DoStatusEffectDamage(unitPresenter.UnitStats, ownerStats);

            float newCurrentDurationTime = currentDurationTime - Time.deltaTime;
            SetCurrentDurationTime(newCurrentDurationTime);
        }
        else
        {
            Destroy(this);
        }
    }


    private void Initialization()
    {
        damageType = new PoisonDamage();
        unitPresenter = gameObject.GetComponent<UnitPresenter>();
        Debug.Log(gameObject.name + @": ""I was poisoned!""");
    }


    public override void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        float newCurrentPoisoningTime = GetCurrentDurationTime() + BaseDurationTime;
        SetCurrentDurationTime(newCurrentPoisoningTime); // Индивидуально

        base.AmplifyEffect(ownerStats, amplificationAmount);

        // Чем больше эффектов мы повесиили на цель, тем сильнее действие модификатора характеристик
        if (myStats != null)
        {
            SetAllModifiersValue();
        }
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats)
    {
        bool isEvaded = false;
        bool isBlocked = false;

        targetStats.TakeDamage(ownerStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, ref isEvaded, ref isBlocked, false);
    }


    private void SetAllModifiersValue()
    {
        SetStrengthModifierValue();
        SetAgilityModifierValue();
        SetMasteryModifierValue();
    }


    private void SetStrengthModifierValue()
    {
        int newValue = (int)(myStats.strength.GetBaseValue() * (decrease * (attributeModifierPercent + attributeModifierIncrease * effectPower)));
        strengthModifierForPoisoning.SetModifierValue(newValue);
    }


    private void SetAgilityModifierValue()
    {
        int newValue = (int)(myStats.agility.GetBaseValue() * (decrease * (attributeModifierPercent + attributeModifierIncrease * effectPower)));
        agilityModifierForPoisoning.SetModifierValue(newValue);
    }


    private void SetMasteryModifierValue()
    {
        int newValue = (int)(myStats.mastery.GetBaseValue() * (decrease * (attributeModifierPercent + attributeModifierIncrease * effectPower)));
        masteryModifierForPoisoning.SetModifierValue(newValue);
    }
}
