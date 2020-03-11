using System;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }

    public float ViewingRadius { get; private set; } = 9f;

    private readonly float hpRegenForStrenght = 0.25f;
    public Stat healthPointRegen;

    private float amountOfMutatedPoints = 2f;
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


    public override void GetExperience(int amount, out bool isLvlUp)
    {
        base.GetExperience((int)(amount / 1.5), out isLvlUp);


        //Если уровень повысился
        if (isLvlUp)
        {
            StatInitialization();
        }
    }


    private protected override void StatInitialization()
    { 
        //Зависимость атрибутов от уровня у врагов

        strength = new Attribute((int)(level.GetLvl() * strenghtFromLvl));
        agility = new Attribute((int)(level.GetLvl() * agilityFromLvl));
        mastery = new Attribute((int)(level.GetLvl() * masteryFromLvl));

        base.StatInitialization();
        healthPointRegen = new Stat(BaseHealthPointRegen + (strength.GetValue() * hpRegenForStrenght));
    }


    public override float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage, out bool isEvaded, out bool isBlocked)
    {
        float returnDamage = base.TakeDamage(killerStats, damageType, damage, out isEvaded, out isBlocked);

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
