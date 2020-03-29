using System.Linq.Expressions;
using UnityEngine;

class Freeze : HangingEffect, IDamageLogic
{
    private DamageType damageType;
    private UnitPresenter unitPresenter;
    private CharacterStats myStats;
    private CharacterStats ownerStats;
    private CharacteristicModifier<float> modifierMovementSpeed = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> modifierRotationSpeed = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> modifierAttackSpeed = new CharacteristicModifier<float>();

    private const float baseDamagePerSecond = 0.1f;
    private const float baseFreezingTime = 5f;

    private float currentFreezingTime;
    private float effectPower = 1f;

    private const float baseDecelerationModifierValue = 0.25f;
    private const float decelerationModifierIncrease = 0.01f;
    private const float defrostPercentTo = 0.9f;
    private const float decrease = -1f; // От finalValue должно ОТНИМАТЬСЯ значение модификатора характеристик эффекта Freeze
    private float defrostPerSecond;
    private float currentDecelerationModifierValue = baseDecelerationModifierValue;


    void Start()
    {
        Initialization();
        unitPresenter.AddStatusEffect(this);


        if (unitPresenter.UnitStats is CharacterStats)
        {
            myStats = (CharacterStats)unitPresenter.UnitStats;
            defrostPerSecond = (baseDecelerationModifierValue * defrostPercentTo) / currentFreezingTime; // Здесь currentFreezingTime = максимальное (эффект только что навесили)
            SetAllModifiersValue();

            myStats.movementSpeed.AddModifier(modifierMovementSpeed);
            myStats.rotationSpeed.AddModifier(modifierMovementSpeed);
            myStats.attackDamage.AddModifier(modifierMovementSpeed);
            
            // Значение модификатора характеристик эффекта Freeze зависит от базового значения характеристик
            myStats.movementSpeed.OnChangeStatBaseValue += SetMovementSpeedModifierValue;
            myStats.rotationSpeed.OnChangeStatBaseValue += SetRotationSpeedModifierValue;
            myStats.attackDamage.OnChangeStatBaseValue += SetAttackSpeedModifierValue;
        }
    }


    void OnDestroy()
    {
        unitPresenter.RemoveStatusEffect(this);

        if (myStats != null)
        {
            myStats.movementSpeed.RemoveModifier(modifierMovementSpeed);
            myStats.rotationSpeed.RemoveModifier(modifierMovementSpeed);
            myStats.attackDamage.RemoveModifier(modifierMovementSpeed);

            myStats.movementSpeed.OnChangeStatBaseValue -= SetMovementSpeedModifierValue;
            myStats.rotationSpeed.OnChangeStatBaseValue -= SetRotationSpeedModifierValue;
            myStats.attackDamage.OnChangeStatBaseValue -= SetAttackSpeedModifierValue;
        }
    }


    void Update()
    {
        if (currentFreezingTime > 0f)
        {
            DoStatusEffectDamage(unitPresenter.UnitStats, ownerStats);
            if (myStats != null)
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
        unitPresenter = gameObject.GetComponent<UnitPresenter>();

        // Сбрасываем огонь
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
        if (unitPresenter.UnitStats is CharacterStats) // Привожу повторно вместо использования myStats т.к. не знаю, что вызовется сначала - AmplifyEffect или Start
        {
            myStats = (CharacterStats)unitPresenter.UnitStats;
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
        unitPresenter.UnitStats.TakeDamage(ownerStats, damageType, remainingDamage, ref isEvaded, ref isBlocked, false);
        Destroy(this);
    }


    private void SetMovementSpeedModifierValue()
    {
        float newValue = myStats.movementSpeed.GetBaseValue() * (decrease * (currentDecelerationModifierValue + decelerationModifierIncrease * effectPower));
        modifierMovementSpeed.SetModifierValue(newValue);
    }


    private void SetRotationSpeedModifierValue()
    {
        float newValue = myStats.rotationSpeed.GetBaseValue() * (decrease * (currentDecelerationModifierValue + decelerationModifierIncrease * effectPower));
        modifierRotationSpeed.SetModifierValue(newValue);
    }


    private void SetAttackSpeedModifierValue()
    {
        float newValue = myStats.attackDamage.GetBaseValue() * (decrease * (currentDecelerationModifierValue + decelerationModifierIncrease * effectPower));
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
