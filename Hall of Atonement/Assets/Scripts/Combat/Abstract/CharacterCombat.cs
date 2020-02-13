﻿using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterCombat : MonoBehaviour
{
    private protected CharacterStats myStats;
    private protected CharacterController controller;

    [HideInInspector] public GameObject targetToAttack = null;

    private float attackCooldown;

    private protected abstract IAttacker Attacker { get; set; } //Сделать абстрактной!


    private protected virtual void Start()
    {
        myStats = GetComponent<CharacterStats>();
        controller = GetComponent<CharacterController>();
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


    private protected virtual void Attack()
    {
        if (targetToAttack != null)
        {
            //Посмотреть на цель
            if (controller.TurnFaceToTarget(targetToAttack, myStats.rotationSpeed.GetValue(), myStats.faceEuler.GetValue()))
            {
                if (attackCooldown <= 0f)
                {
                    Attacker.Attack();
                    attackCooldown = 1f / myStats.attackSpeed.GetValue(); //После атаки включить кулдаун
                }
            }
        }
        else
        {
            if (attackCooldown <= 0f)
            {
                Attacker.Attack();
                attackCooldown = 1f / myStats.attackSpeed.GetValue(); //После атаки включить кулдаун
            }
        }
    }


    //Статы врага нужны, что бы можно было нанести урон. Свои статы нужны, что бы можно было получить опыт
    public void DoDamage(UnitStats targetStats)
    {
        //Формула, которая повысит урон в случае, если скорость атак будет быстрее чем обновление кадров
        float attackSpeedMultiplie = (Mathf.Abs(attackCooldown) / (1f/ myStats.attackSpeed.GetValue())) + 1f;

        float damage = attackSpeedMultiplie * myStats.attackDamage.GetValue();

        //Если Крит. шанс больше нуля И если рандом говорит о том, что нужно критануть
        if (myStats.criticalChance.GetValue() > 0f && Random.Range(0f, 100f) < myStats.criticalChance.GetValue())
        {
            damage *= myStats.criticalMultilpie.GetValue(); //То увеличить урон
        }


        //Есть ли модификатор атаки?


        targetStats.TakeDamage(myStats, damage);
    }
}