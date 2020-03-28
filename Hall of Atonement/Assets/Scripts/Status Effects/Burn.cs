using UnityEngine;

public class Burn : HangingEffect, IDamageLogic
{
    public DamageType damageType;
    private UnitPresenter unitPresenter;
    private CharacterStats ownerStats;

    private readonly float baseDamagePerSecond = 2f;
    private readonly float baseBurningTime = 3f;

    private float effectPower = 1f;
    private float currentBurningTime;


    void Start()
    {
        Initialization();
        unitPresenter.AddStatusEffect(this);
    }


    void OnDestroy()
    {
        unitPresenter.RemoveStatusEffect(this);
    }


    void Update()
    {
        if (currentBurningTime > 0f)
        {
            DoStatusEffectDamage(unitPresenter.UnitStats, ownerStats);
            currentBurningTime -= Time.deltaTime;
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
        this.ownerStats = ownerStats;
        currentBurningTime = baseBurningTime;
        effectPower += amplificationAmount;
    }


    public void SelfDestruction()
    {
        bool isEvaded = false;
        bool isBlocked = false;

        float remainingDamage = baseDamagePerSecond * effectPower * currentBurningTime;
        unitPresenter.UnitStats.TakeDamage(ownerStats, damageType, remainingDamage, ref isEvaded, ref isBlocked, false);
        Destroy(this);
    }
}
