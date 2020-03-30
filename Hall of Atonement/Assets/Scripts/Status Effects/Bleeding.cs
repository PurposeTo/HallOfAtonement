using UnityEngine;

class Bleeding : HangingEffect, IDamageLogic
{
    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Bleeding;

    Sprite IStatusEffectLogic.StatusEffectSprite => GameManager.instance.GetStatusEffectData(StatusEffectType).StatusEffectSprite;

    private DamageType damageType;
    private UnitPresenter unitPresenter;
    private CharacterStats ownerStats;
    private CharacteristicModifier<float> poisonResistanceForBleeding = new CharacteristicModifier<float>(0.1f);

    private readonly float baseDamagePerSecond = 0.25f;
    private readonly float baseBleedingTime = 5f;
    private const float poisonResistanceIncrease = 0.01f;

    private float currentBleedingTime;
    private float effectPower = 1f;

    void Start()
    {
        Initialization();
        unitPresenter.AddStatusEffect(this);

        unitPresenter.UnitStats.poisonResistance.AddModifier(poisonResistanceForBleeding);
    }


    void OnDestroy()
    {
        unitPresenter.RemoveStatusEffect(this);

        unitPresenter.UnitStats.poisonResistance.RemoveModifier(poisonResistanceForBleeding);
    }


    void Update()
    {
        if (currentBleedingTime > 0f)
        {
            DoStatusEffectDamage(unitPresenter.UnitStats, ownerStats);
            currentBleedingTime -= Time.deltaTime;
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
        this.ownerStats = ownerStats;
        currentBleedingTime += baseBleedingTime;
        effectPower += amplificationAmount;
        poisonResistanceForBleeding.SetModifierValue(poisonResistanceForBleeding.GetModifierValue() + poisonResistanceIncrease * effectPower);
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownersStats)
    {
        bool isEvaded = false;
        bool isBlocked = false;

        targetStats.TakeDamage(ownersStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, ref isEvaded, ref isBlocked, false);
    }

    void IDamageLogic.DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats)
    {
        throw new System.NotImplementedException();
    }
}
