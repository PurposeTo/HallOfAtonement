﻿using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterCombat : MonoBehaviour
{
    private protected CharacterStats myStats;
    private protected CharacterController controller;
    private protected virtual DamageType damageType { get; set; }

    [HideInInspector] public GameObject targetToAttack = null;

    private float attackCooldown;

    private protected IAttacker Attacker { get; set; }


    private protected virtual void Start()
    {
        myStats = GetComponent<CharacterStats>();
        controller = GetComponent<CharacterController>();
        damageType = new PhysicalDamage(myStats.attackDamage.GetValue()); //сейчас все атакуют физ.уроном!!!
    }


    private protected virtual void Update()
    {
        if (attackCooldown > 0f) //Если кулдаун больше нуля, то уменьшить
        {
            attackCooldown -= Time.deltaTime;
        }
        else if (targetToAttack == null && attackCooldown < 0f) //Если игрок сейчас НЕ атакует и кулдаун меньше нуля, то сбросить кулдаун
        {
            attackCooldown = 0f;
        }
    }


    public abstract void SearchingTargetToAttack(GameObject targetAttack);


    private protected virtual void PreAttack()
    {
        if (targetToAttack != null)
        {
            //Посмотреть на цель
            if (controller.TurnFaceToTarget(targetToAttack, myStats.rotationSpeed.GetValue(), myStats.faceEuler))
            {
                if (attackCooldown <= 0f)
                {
                    Attacker.Attack(this);
                    attackCooldown = 1f / myStats.attackSpeed.GetValue(); //После атаки включить кулдаун
                }
            }
        }
        else
        {
            if (attackCooldown <= 0f)
            {
                Attacker.Attack(this);
                attackCooldown = 1f / myStats.attackSpeed.GetValue(); //После атаки включить кулдаун
            }
        }
    }


    //Статы врага нужны, что бы можно было нанести урон. Свои статы нужны, что бы можно было получить опыт
    public void DoDamage(UnitStats targetStats)
    {
        //Формула, которая повысит урон в случае, если скорость атак будет быстрее чем обновление кадров
        float attackSpeedMultiplie = (Mathf.Abs(attackCooldown) / (1f/ myStats.attackSpeed.GetValue())) + 1f;

        float damage = attackSpeedMultiplie * damageType.Damage;

        //Если Крит. шанс больше нуля И если рандом говорит о том, что нужно критануть
        if (myStats.criticalChance.GetValue() > 0f && Random.Range(1f, 100f) <= myStats.criticalChance.GetValue())
        {
            damage *= myStats.criticalMultiplier.GetValue(); //То увеличить урон
        }

        //Есть ли модификатор атаки?


        targetStats.TakeDamage(myStats, damageType, damage);
    }
}
