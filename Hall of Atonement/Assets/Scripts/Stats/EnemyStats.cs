using System;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }

    public float ViewingRadius { get; private set; } = 9f;

    private readonly float baseHealthPointRegen = 0.5f; //базовое значение регенерации здоровья

    private readonly float hpRegenForStrenght = 0.25f;
    public Stat healthPointRegen;

    private float amountOfMutatedPoints = 1.2f;
    private float strenghtFromLvl;
    private float agilityFromLvl;
    private float masteryFromLvl;

    private protected override float BaseMovementSpeed { get; } = 3f;
    private protected override float BaseRotationSpeed { get; } = 360f;


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ViewingRadius);
    }


    private protected override void Awake()
    {
        Mutate(amountOfMutatedPoints);
        base.Awake();
    }

    private protected override void Start()
    {
        base.Start();

        EnemyPresenter = (EnemyPresenter)CharacterPresenter;

        GameManager.instance.enemys.Add(gameObject);
    }

    private void OnDisable()
    {
        GameManager.instance.enemys.Remove(gameObject);
    }


    private protected virtual void Update()
    {
        Healing(healthPointRegen.GetValue() * Time.deltaTime);
    }


    private void Mutate(float amountOfMutatedPoints)
    {
        float[] pointsForLvl = new float[3];

        float remainingPointsCounter = amountOfMutatedPoints;
        for (int i = 0; i < pointsForLvl.Length - 1; i++)
        {
            float currentMutatedPoints = UnityEngine.Random.Range(0, remainingPointsCounter);
            pointsForLvl[i] = (float)Math.Round(currentMutatedPoints, 2);
            remainingPointsCounter -= currentMutatedPoints;
        }

        //Значение в последнем поинте равно оставшемуся значению в count;
        pointsForLvl[pointsForLvl.Length - 1] = remainingPointsCounter;

        GameLogic.Shuffle(pointsForLvl);

        strenghtFromLvl = pointsForLvl[0];
        agilityFromLvl = pointsForLvl[1];
        masteryFromLvl = pointsForLvl[2];
    }


    public override void GetExperience(int amount, out int numberOfNewLvls)
    {
        base.GetExperience((int)(amount / 1.5), out numberOfNewLvls);


        //Если уровень повысился
        if (numberOfNewLvls > 0)
        {
            strength.ChangeBaseValue((int)(level.GetLvl() * strenghtFromLvl));

            agility.ChangeBaseValue((int)(level.GetLvl() * agilityFromLvl));

            mastery.ChangeBaseValue((int)(level.GetLvl() * masteryFromLvl));
        }
    }


    private protected override void StatInitialization()
    { 
        //Зависимость атрибутов от уровня у врагов

        strength = new Attribute((int)(level.GetLvl() * strenghtFromLvl));
        agility = new Attribute((int)(level.GetLvl() * agilityFromLvl));
        mastery = new Attribute((int)(level.GetLvl() * masteryFromLvl));

        base.StatInitialization();
        healthPointRegen = new Stat(baseHealthPointRegen + (strength.GetValue() * hpRegenForStrenght));
    }


    private protected override void UpdateBaseStrenghtStatsValue()
    {
        base.UpdateBaseStrenghtStatsValue();

        healthPointRegen.ChangeBaseValue(baseHealthPointRegen + (strength.GetValue() * hpRegenForStrenght));
    }


    public override float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked, bool canEvade = true)
    {
        float returnDamage = base.TakeDamage(killerStats, damageType, damage, ref isEvaded, ref isBlocked, canEvade);

        if (!isEvaded && killerStats != null)
        {
            EnemyPresenter.EnemyAI.EnemyAIStateMachine.Fighting(EnemyPresenter.EnemyAI, killerStats.gameObject);
        }

        return returnDamage;

    }


    public override void Die(CharacterStats killerStats)
    {
        base.Die(killerStats);
        Destroy(gameObject);
    }
}
