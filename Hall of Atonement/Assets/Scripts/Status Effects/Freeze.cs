using UnityEngine;

class Freeze : ItemHarding, IDamageLogic
{
    private DamageType damageType;
    private UnitStats targetStats;
    private CharacterStats ownerStats;

    private readonly float baseDamagePerSecond = 1f;
    private readonly float baseFreezingTime = 3f;

    private float currentFreezingTime;
    private float effectPower;

    private readonly float decelerationPercent = 0.6f;
    private readonly float defrostPercentTo = 0.75f;
    private float defrostPerSecond;


    void Start()
    {
        Initialization();
    }


    void Update()
    {
        DoStatusEffectDamage(targetStats, ownerStats);
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


    public void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        this.ownerStats = ownerStats;
        currentFreezingTime += baseFreezingTime; //увеличить время действия на (1)
        defrostPerSecond = (decelerationPercent * defrostPercentTo) / currentFreezingTime;
        effectPower += amplificationAmount;
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats)
    {
        if (currentFreezingTime > 0f)
        {
            bool isEvaded = false;
            bool isBlocked = false;

            targetStats.TakeDamage(ownerStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, false, ref isEvaded, ref isBlocked);
            currentFreezingTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this);
        }
    }


    public void SelfDestruction()
    {
        bool isEvaded = false;
        bool isBlocked = false;

        float remainingDamage = baseDamagePerSecond * effectPower * currentFreezingTime;
        targetStats.TakeDamage(ownerStats, damageType, remainingDamage, false, ref isEvaded, ref isBlocked);
        Destroy(this);
    }
}
