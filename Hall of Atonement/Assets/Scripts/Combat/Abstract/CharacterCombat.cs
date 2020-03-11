using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
public abstract class CharacterCombat : MonoBehaviour
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }
    //private protected CharacterStats myStats;
    //private protected CharacterController controller;

    [HideInInspector] public GameObject targetToAttack = null;

    private float attackCooldown;

    public IAttacker Attacker { get; private protected set; }

    private IDamageReducerFactory statusEffectFactory;

    public List<IAttackModifier> attackModifiers = new List<IAttackModifier>();


    private protected virtual void Start()
    {
        CharacterPresenter = GetComponent<CharacterPresenter>();
        //myStats = GetComponent<CharacterStats>();
        //controller = GetComponent<CharacterController>();
        Attacker = GetComponent<IAttacker>();
        statusEffectFactory = new DamageReducerFactory();
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


    private protected virtual void PreAttack(GameObject target)
    {
        targetToAttack = target;

        if (target != null)
        {
            //Посмотреть на цель
            if (CharacterPresenter.Controller.TurnFaceToTarget(
                targetToAttack, CharacterPresenter.MyStats.rotationSpeed.GetValue(),
                CharacterPresenter.MyStats.faceEuler))
            {
                if (attackCooldown <= 0f)
                {
                    Attacker.Attack(this);
                    attackCooldown = 1f / CharacterPresenter.MyStats.attackSpeed.GetValue(); //После атаки включить кулдаун
                }
            }
        }
        else
        {
            if (attackCooldown <= 0f)
            {
                Attacker.Attack(this);
                attackCooldown = 1f / CharacterPresenter.MyStats.attackSpeed.GetValue(); //После атаки включить кулдаун
            }
        }
    }


    //Статы врага нужны, что бы можно было нанести урон. Свои статы нужны, что бы можно было получить опыт
    public void DoDamage(UnitStats targetStats)
    {
        //Формула, которая повысит урон в случае, если скорость атак будет быстрее чем обновление кадров
        float attackSpeedMultiplie = (Mathf.Abs(attackCooldown) / (1f / CharacterPresenter.MyStats.attackSpeed.GetValue())) + 1f;

        float damage = attackSpeedMultiplie * CharacterPresenter.MyStats.attackDamage.GetValue();

        //Если Крит. шанс больше нуля И если рандом говорит о том, что нужно критануть
        if (CharacterPresenter.MyStats.criticalChance.GetValue() > 0f && Random.Range(1f, 100f)
            <= CharacterPresenter.MyStats.criticalChance.GetValue())
        {
            damage *= CharacterPresenter.MyStats.criticalMultiplier.GetValue(); //То увеличить урон
        }


        damage = targetStats.TakeDamage(CharacterPresenter.MyStats, CharacterPresenter.MyStats.DamageType, damage, true, out bool isEvaded, out bool isBlocked);

        if (!isEvaded)
        {
            if (!isBlocked) //Если урон не был полностью заблокирован 
            {
                if (CharacterPresenter.MyStats.DamageType is EffectDamage)
                {
                    StatusEffectFactory statusEffectFactory = new StatusEffectFactory();
                    statusEffectFactory.HangStatusEffect(CharacterPresenter.MyStats.DamageType, targetStats, CharacterPresenter.MyStats);
                }
            }


            /*
             * Есть ли модификатор атаки?
             * 
             * Если у цели есть модификатор атаки, который должен навесить дебафф, то сам модификатор проверяет, может ли он это сделать.
             */



            for (int i = 0; i < attackModifiers.Count; i++)
            {
                attackModifiers[i].ApplyAttackModifier(damage, CharacterPresenter.MyStats.mastery.GetValue());
            }
        }
    }
}
