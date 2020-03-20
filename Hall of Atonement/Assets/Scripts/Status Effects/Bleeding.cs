using UnityEngine;

class Bleeding : HangingEffect, IDamageLogic
{
    private DamageType damageType;
    private UnitStats targetStats;
    private CharacterStats ownerStats;
    private CharacteristicModifier<float> poisonResistanceForBleeding = new CharacteristicModifier<float>(0.1f);

    private readonly float baseDamagePerSecond = 0.5f;
    private readonly float baseBleedingTime = 5f;
    private const float poisonResistanceIncrease = 0.01f;

    private float currentBleedingTime;
    private float effectPower = 1f;


    void Start()
    {
        Initialization();
        targetStats.poisonResistance.AddModifier(poisonResistanceForBleeding);
    }


    void OnDestroy()
    {
        targetStats.poisonResistance.RemoveModifier(poisonResistanceForBleeding);
    }


    void Update()
    {
        if (currentBleedingTime > 0f)
        {
            DoStatusEffectDamage(targetStats, ownerStats);
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
        targetStats = gameObject.GetComponent<UnitStats>();
        Debug.Log(gameObject.name + @": ""I am bleeding!""");
    }


    public override void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        this.ownerStats = ownerStats;
        currentBleedingTime += baseBleedingTime;
        effectPower += amplificationAmount;
        poisonResistanceForBleeding.SetModifierValue(poisonResistanceForBleeding.GetModifierValue() + poisonResistanceIncrease);
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownersStats)
    {
        bool isEvaded = false;
        bool isBlocked = false;

        targetStats.TakeDamage(ownersStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, ref isEvaded, ref isBlocked, false);
    }
}
