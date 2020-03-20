using UnityEngine;

class Freeze : HangingEffect, IDamageLogic
{
    private DamageType damageType;
    private UnitStats targetStats;
    private CharacterStats ownerStats;
    private CharacteristicModifier<float> modifierMovementSpeed = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> modifierRotationSpeed = new CharacteristicModifier<float>();
    private CharacteristicModifier<float> modifierAttackSpeed = new CharacteristicModifier<float>();

    private readonly float baseDamagePerSecond = 0.25f;
    private readonly float baseFreezingTime = 4f;

    private float currentFreezingTime;
    private float effectPower = 1f;

    private readonly float decelerationPercent = 0.6f;
    private readonly float defrostPercentTo = 0.75f;
    private float defrostPerSecond;


    void Start()
    {
        Initialization();
    }


    void Update()
    {
        if (currentFreezingTime > 0f)
        {
            DoStatusEffectDamage(targetStats, ownerStats);
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
        defrostPerSecond = (decelerationPercent * defrostPercentTo) / currentFreezingTime;
        effectPower += amplificationAmount;
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
}
