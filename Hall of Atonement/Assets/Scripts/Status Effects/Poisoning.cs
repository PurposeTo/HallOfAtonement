using UnityEngine;

class Poisoning : HangingEffect, IDamageLogic
{
    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Poisoning;

    Sprite IStatusEffectLogic.StatusEffectSprite => GameManager.instance.GetStatusEffectData(StatusEffectType).StatusEffectSprite;

    private UnitPresenter unitPresenter;

    private CharacterStats myStats;
    private CharacterStats ownerStats;
    private DamageType damageType;
    private CharacteristicModifier<int> strengthModifierForPoisoning = new CharacteristicModifier<int>();
    private CharacteristicModifier<int> agilityModifierForPoisoning = new CharacteristicModifier<int>();
    private CharacteristicModifier<int> masteryModifierForPoisoning = new CharacteristicModifier<int>();

    private readonly float baseDamagePerSecond = 0.1f;
    private readonly float basePoisoningTime = 5f;

    private const float attributeModifierPercent = 0.1f;
    private const float attributeModifierIncrease = 0.01f;
    private const float decrease = -1f; // от finalValue должно ОТНИМАТЬСЯ значение модификатора характеристик эффекта Poisoning

    private float currentPoisoningTime;
    private float effectPower = 1f;


    void Start()
    {
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


    void OnDestroy()
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


    void Update()
    {
        if (currentPoisoningTime > 0f)
        {
            DoStatusEffectDamage(unitPresenter.UnitStats, ownerStats);
            currentPoisoningTime -= Time.deltaTime;
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
        this.ownerStats = ownerStats;
        currentPoisoningTime += basePoisoningTime;
        effectPower += amplificationAmount;

        // Чем больше эффектов мы повесиили на цель, тем сильнее действие модификатора характеристик
        if (unitPresenter.UnitStats is CharacterStats) // Привожу повторно вместо использования myStats т.к. не знаю, что вызовется сначала - AmplifyEffect или Start
        {
            myStats = (CharacterStats)unitPresenter.UnitStats;
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
