﻿using UnityEngine;

class Bleeding : MonoBehaviour, IDamageLogic
{
    private DamageType damageType;
    private UnitStats targetStats;
    private CharacterStats ownerStats;

    private readonly float baseDamagePerSecond = 4f;
    private readonly float baseBleedingTime = 2f;

    private float currentBleedingTime;
    private float effectPower;


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
        damageType = new BleedingDamage();
        targetStats = gameObject.GetComponent<UnitStats>();
        Debug.Log(gameObject.name + @": ""I am bleeding!""");
    }


    public void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        this.ownerStats = ownerStats;
        currentBleedingTime += baseBleedingTime;
        effectPower += amplificationAmount;
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownersStats)
    {
        if (currentBleedingTime > 0f)
        {
            targetStats.TakeDamage(ownersStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, false, out bool _, out bool _);
            currentBleedingTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this);
        }
    }
}
