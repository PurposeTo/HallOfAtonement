using UnityEngine;

public class Burn : ActiveEffect, IDamageLogic
{

    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Burn;
    public override StatusEffectData StatusEffectData => StatusEffectDataContainer.Instance.GetStatusEffectData(StatusEffectType);


    public DamageType damageType;
    private UnitPresenter unitPresenter;

    private protected override float BaseDurationTime => 3f;
    private readonly float baseDamagePerSecond = 0.75f;


    private void Awake()
    {
        // Внимание! Инициализация должна быть строго в Awake, так как он вызывается до AmplifyEffect
        Initialization();
        unitPresenter.AddStatusEffect(this);
    }


    private void OnDestroy()
    {
        unitPresenter.RemoveStatusEffect(this);
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
        damageType = new FireDamage();
        Debug.Log(gameObject.name + ": \"I am burning!\"");
        unitPresenter = gameObject.GetComponent<UnitPresenter>();


        // Проверить, есть ли на цели лед. Если есть, то разморозить.
        if (unitPresenter.UnitStats.gameObject.TryGetComponent(out Freeze freeze))
        {
            freeze.SelfDestruction();
        }
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats)
    {
        bool isEvaded = false;
        bool isBlocked = false;

        targetStats.TakeDamage(ownerStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, ref isEvaded, ref isBlocked, false);
    }


    public override void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        SetCurrentDurationTime(BaseDurationTime); // Индивидуально

        base.AmplifyEffect(ownerStats, amplificationAmount);
    }


    public void SelfDestruction()
    {
        bool isEvaded = false;
        bool isBlocked = false;

        float remainingDamage = baseDamagePerSecond * effectPower * GetCurrentDurationTime();
        unitPresenter.UnitStats.TakeDamage(ownerStats, damageType, remainingDamage, ref isEvaded, ref isBlocked, false);
        Destroy(this);
    }
}
