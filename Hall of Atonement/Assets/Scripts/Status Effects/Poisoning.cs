using UnityEngine;

class Poisoning : HangingEffect, IDamageLogic
{
    private UnitStats targetStats;
    private CharacterStats ownerStats;
    private DamageType damageType;
    private CharacteristicModifier<int> strengthModifierForPoisoning = new CharacteristicModifier<int>();
    private CharacteristicModifier<int> agilityModifierForPoisoning = new CharacteristicModifier<int>();
    private CharacteristicModifier<int> masteryModifierForPoisoning = new CharacteristicModifier<int>();

    private readonly float baseDamagePerSecond = 0.25f;
    private readonly float basePoisoningTime = 4f;

    private const float attributeModifierPercent = 0.1f;
    private const float attributeModifierIncrease = 0.01f;
    private const float decrease = -1f; // от finalValue должно ОТНИМАТЬСЯ значение модификатора характеристик эффекта Poisoning

    private float currentPoisoningTime;
    private float effectPower = 1f;


    void Start()
    {
        Initialization();

        if (targetStats is CharacterStats)
        {
            SetAllModifiersValue();

            ((CharacterStats)targetStats).strength.AddModifier(strengthModifierForPoisoning);
            ((CharacterStats)targetStats).agility.AddModifier(agilityModifierForPoisoning);
            ((CharacterStats)targetStats).mastery.AddModifier(masteryModifierForPoisoning);

            // Значение модификатора характеристик эффекта Poisoning меняется в соответствии с базовым значением характеристик
            ((CharacterStats)targetStats).strength.OnChangeAttributeBaseValue += SetStrengthModifierValue;
            ((CharacterStats)targetStats).agility.OnChangeAttributeBaseValue += SetAgilityModifierValue;
            ((CharacterStats)targetStats).mastery.OnChangeAttributeBaseValue += SetMasteryModifierValue;
        }
    }


    void OnDestroy()
    {
        if (targetStats is CharacterStats)
        {
            ((CharacterStats)targetStats).strength.RemoveModifier(strengthModifierForPoisoning);
            ((CharacterStats)targetStats).agility.RemoveModifier(agilityModifierForPoisoning);
            ((CharacterStats)targetStats).mastery.RemoveModifier(masteryModifierForPoisoning);

            ((CharacterStats)targetStats).strength.OnChangeAttributeBaseValue -= SetStrengthModifierValue;
            ((CharacterStats)targetStats).agility.OnChangeAttributeBaseValue -= SetAgilityModifierValue;
            ((CharacterStats)targetStats).mastery.OnChangeAttributeBaseValue -= SetMasteryModifierValue;
        }
    }


    void Update()
    {
        if (currentPoisoningTime > 0f)
        {
            DoStatusEffectDamage(targetStats, ownerStats);
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
        targetStats = gameObject.GetComponent<UnitStats>();
        Debug.Log(gameObject.name + @": ""I was poisoned!""");
    }


    public override void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        this.ownerStats = ownerStats;
        currentPoisoningTime += basePoisoningTime;
        effectPower += amplificationAmount;

        // Чем больше эффектов мы повесиили на цель, тем сильнее действие модификатора характеристик
        if (targetStats is CharacterStats)
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
        int newValue = (int)(((CharacterStats)targetStats).strength.GetBaseValue() * (decrease * (attributeModifierPercent + attributeModifierIncrease * effectPower)));
        strengthModifierForPoisoning.SetModifierValue(newValue);
    }


    private void SetAgilityModifierValue()
    {
        int newValue = (int)(((CharacterStats)targetStats).agility.GetBaseValue() * (decrease * (attributeModifierPercent + attributeModifierIncrease * effectPower)));
        agilityModifierForPoisoning.SetModifierValue(newValue);
    }


    private void SetMasteryModifierValue()
    {
        int newValue = (int)(((CharacterStats)targetStats).mastery.GetBaseValue() * (decrease * (attributeModifierPercent + attributeModifierIncrease * effectPower)));
        masteryModifierForPoisoning.SetModifierValue(newValue);
    }
}
