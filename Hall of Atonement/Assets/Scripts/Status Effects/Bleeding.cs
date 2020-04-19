using UnityEngine;

class Bleeding : ActiveEffect, IDamageLogic
{
    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Bleeding;

    public override StatusEffectData StatusEffectData => StatusEffectDataContainer.Instance.GetStatusEffectData(StatusEffectType);

    private UnitPresenter unitPresenter;
    private DamageType damageType;

    private CharacteristicModifier<float> poisonResistanceForBleeding = new CharacteristicModifier<float>(0.1f);

    private protected override float BaseDurationTime => 5f;
    private readonly float baseDamagePerSecond = 0.5f;
    private const float poisonResistanceIncrease = 0.01f;


    private void Awake()
    {
        // Внимание! Инициализация должна быть строго в Awake, так как он вызывается до AmplifyEffect
        Initialization();
        unitPresenter.AddStatusEffect(this);

        unitPresenter.UnitStats.poisonResistance.AddModifier(poisonResistanceForBleeding);
    }


    private void OnDestroy()
    {
        unitPresenter.RemoveStatusEffect(this);

        unitPresenter.UnitStats.poisonResistance.RemoveModifier(poisonResistanceForBleeding);
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
        damageType = new BleedingDamage();

        unitPresenter = gameObject.GetComponent<UnitPresenter>();
        Debug.Log(gameObject.name + @": ""I am bleeding!""");
    }


    public override void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        float newCurrentPoisoningTime = GetCurrentDurationTime() + BaseDurationTime;
        SetCurrentDurationTime(newCurrentPoisoningTime); // Индивидуально

        base.AmplifyEffect(ownerStats, amplificationAmount);

        poisonResistanceForBleeding.SetModifierValue(poisonResistanceForBleeding.GetModifierValue() + poisonResistanceIncrease * effectPower);
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownersStats)
    {
        bool isEvaded = false;
        bool isBlocked = false;

        targetStats.TakeDamage(ownersStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, ref isEvaded, ref isBlocked, false);
    }
}
