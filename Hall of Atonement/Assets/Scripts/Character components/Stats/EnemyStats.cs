﻿using System;
using UnityEngine;

public class EnemyStats : CharacterStats
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }

    public float ViewingRadius { get; } = 9f;

    protected override float BaseChanceToGetAnExtraSkillPoint { get; } = 0.2f;

    private protected override float BaseMovementSpeed { get; } = 5f; // 3f
    private protected override float BaseRotationSpeed { get; } = 520f; // 360f


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, ViewingRadius);
    }


    private protected override void Awake()
    {
        base.Awake();
        Mutate();
    }

    private protected override void Start()
    {
        base.Start();

        EnemyPresenter = (EnemyPresenter)CharacterPresenter;

        RoomController.Instance.AddEnemyToAllEnemysList(gameObject);
    }

    private void OnDisable()
    {
        RoomController.Instance.RemoveEnemyFromAllEnemysList(gameObject);
    }


    private void Mutate()
    {
        // Данный метод должен изменять массу атрибута

        float[] massPerAttribute = new float[AllAttributes.Length];
        float remainingPointsCounter = AllAttributes.Length; // Единица на каждый атрибут, для простоты счета

        for (int i = 0; i < massPerAttribute.Length - 1; i++)
        {
            float currentMutatedPoints = UnityEngine.Random.Range(0, remainingPointsCounter);
            massPerAttribute[i] = (float)Math.Round(currentMutatedPoints, 2);
            remainingPointsCounter -= currentMutatedPoints;
        }

        //Значение в последнем поинте равно оставшемуся значению в count;
        massPerAttribute[massPerAttribute.Length - 1] = (float)Math.Round(remainingPointsCounter, 2);

        GameLogic.Shuffle(massPerAttribute);

        for (int i = 0; i < massPerAttribute.Length; i++)
        {
            AllAttributes[i].SetMassPerAtribute(massPerAttribute[i]);
        }
    }


    public override void GetExperience(int amount)
    {
        base.GetExperience((int)(amount / 1.5));
    }


    public override float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked, bool canEvade = true, bool isCritical = false, bool displayPopup = false)
    {
        float returnDamage = base.TakeDamage(killerStats, damageType, damage, ref isEvaded, ref isBlocked, canEvade, isCritical, displayPopup);

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
