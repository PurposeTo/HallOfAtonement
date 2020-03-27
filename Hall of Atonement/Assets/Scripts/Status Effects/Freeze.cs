using System.Linq.Expressions;
using UnityEngine;

class Freeze : HangingEffect, IDamageLogic
{
    private DamageType damageType;
    private UnitStats targetStats;
    private CharacterStats ownerStats;
    private CharacteristicModifier<float> modifierMovementSpeed = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> modifierRotationSpeed = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> modifierAttackSpeed = new CharacteristicModifier<float>();

    private const float baseDamagePerSecond = 0.25f;
    private const float baseFreezingTime = 4f;

    private float currentFreezingTime;
    private float effectPower = 1f;

    private const float baseDecelerationModifierValue = 0.1f;
    private const float decelerationModifierIncrease = 0.01f;
    private const float defrostPercentTo = 0.75f;
    private const float decrease = -1f; // от finalValue должно ОТНИМАТЬСЯ значение модификатора характеристик эффекта Freeze
    private float defrostPerSecond;
    private float currentDecelerationModifierValue = baseDecelerationModifierValue;


    void Start()
    {
        Initialization();

        if (targetStats is CharacterStats)
        {
            defrostPerSecond = (baseDecelerationModifierValue * defrostPercentTo) / currentFreezingTime; // Здесь currentFreezingTime = максимальное (эффект только что навесили)
            SetAllModifiersValue();

            ((CharacterStats)targetStats).movementSpeed.AddModifier(modifierMovementSpeed);
            ((CharacterStats)targetStats).rotationSpeed.AddModifier(modifierMovementSpeed);
            ((CharacterStats)targetStats).attackDamage.AddModifier(modifierMovementSpeed);

            // Значение модификатора характеристик эффекта Freeze зависит от базового значения характеристик
            ((CharacterStats)targetStats).movementSpeed.OnChangeStatBaseValue += SetMovementSpeedModifierValue;
            ((CharacterStats)targetStats).rotationSpeed.OnChangeStatBaseValue += SetRotationSpeedModifierValue;
            ((CharacterStats)targetStats).attackDamage.OnChangeStatBaseValue += SetAttackSpeedModifierValue;
        }
    }


    void OnDestroy()
    {
        if (targetStats is CharacterStats)
        {
            ((CharacterStats)targetStats).movementSpeed.RemoveModifier(modifierMovementSpeed);
            ((CharacterStats)targetStats).rotationSpeed.RemoveModifier(modifierMovementSpeed);
            ((CharacterStats)targetStats).attackDamage.RemoveModifier(modifierMovementSpeed);

            ((CharacterStats)targetStats).movementSpeed.OnChangeStatBaseValue -= SetMovementSpeedModifierValue;
            ((CharacterStats)targetStats).rotationSpeed.OnChangeStatBaseValue -= SetRotationSpeedModifierValue;
            ((CharacterStats)targetStats).attackDamage.OnChangeStatBaseValue -= SetAttackSpeedModifierValue;
        }
    }


    void Update()
    {
        if (currentFreezingTime > 0f)
        {
            DoStatusEffectDamage(targetStats, ownerStats);
            if (targetStats is CharacterStats)
            {
                currentDecelerationModifierValue -= defrostPerSecond * Time.deltaTime; // Значение уменьшается => при повторном вызове значения хар-к увеличиваются
                SetAllModifiersValue(); // Оттаивание
            }
            currentFreezingTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this);
        }
    }


    private void Initialization()
    {
        damageType = new IceDamage();
        targetStats = gameObject.GetComponent<UnitStats>();

        //сбрасываем огонь
        if (gameObject.TryGetComponent(out Burn burn))
        {
            burn.SelfDestruction();
        }

        Debug.Log(gameObject.name + @": ""I was frozen!""");
    }


    public override void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount) 
    {
        this.ownerStats = ownerStats;
        currentFreezingTime += baseFreezingTime; // Увеличить время действия на (1)
        effectPower += amplificationAmount;

        // Чем больше эффектов мы повесили на цель, тем сильнее действие модификатора характеристик
        if (targetStats is CharacterStats)
        {
            defrostPerSecond = (baseDecelerationModifierValue * defrostPercentTo) / currentFreezingTime; // Здесь currentFreezingTime = максимальное текущее от кол-ва навешанных эффектов
            AmplifyAllModifiersValue();
        }
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats)
    {
        bool isEvaded = false;
        bool isBlocked = false;

        targetStats.TakeDamage(ownerStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, ref isEvaded, ref isBlocked, false);
    }


    public void SelfDestruction()
    {
        bool isEvaded = false;
        bool isBlocked = false;

        float remainingDamage = baseDamagePerSecond * effectPower * currentFreezingTime;
        targetStats.TakeDamage(ownerStats, damageType, remainingDamage, ref isEvaded, ref isBlocked, false);
        Destroy(this);
    }


    private void SetMovementSpeedModifierValue()
    {
        float newValue = ((CharacterStats)targetStats).movementSpeed.GetBaseValue() * (decrease * (currentDecelerationModifierValue + decelerationModifierIncrease * effectPower));
        modifierMovementSpeed.SetModifierValue(newValue);
    }


    private void SetRotationSpeedModifierValue()
    {
        float newValue = ((CharacterStats)targetStats).rotationSpeed.GetBaseValue() * (decrease * (currentDecelerationModifierValue + decelerationModifierIncrease * effectPower));
        modifierRotationSpeed.SetModifierValue(newValue);
    }


    private void SetAttackSpeedModifierValue()
    {
        float newValue = ((CharacterStats)targetStats).attackDamage.GetBaseValue() * (decrease * (currentDecelerationModifierValue + decelerationModifierIncrease * effectPower));
        modifierAttackSpeed.SetModifierValue(newValue);
    }


    private void SetAllModifiersValue()
    {
        SetMovementSpeedModifierValue();
        SetRotationSpeedModifierValue();
        SetAttackSpeedModifierValue();
    }


    private void AmplifyAllModifiersValue()
    {
        float newValue = modifierMovementSpeed.GetModifierValue() + (decrease * (decelerationModifierIncrease * effectPower));
        modifierMovementSpeed.SetModifierValue(newValue);
        newValue = modifierAttackSpeed.GetModifierValue() + (decrease * (decelerationModifierIncrease * effectPower));
        modifierAttackSpeed.SetModifierValue(newValue);
        newValue = modifierRotationSpeed.GetModifierValue() + (decrease * (decelerationModifierIncrease * effectPower));
        modifierRotationSpeed.SetModifierValue(newValue);
    }
}
