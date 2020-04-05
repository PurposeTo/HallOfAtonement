using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterMovement))]
public abstract class CharacterCombat : UnitCombat
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }

    [HideInInspector] public GameObject targetToAttack = null;

    private float attackCooldown;

    public IAttacker Attacker { get; private protected set; }


    private protected virtual void Start()
    {
        CharacterPresenter = GetComponent<CharacterPresenter>();

        Attacker = GetComponent<IAttacker>();
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


    private protected void PreAttack(GameObject target)
    {
        targetToAttack = target;

        //Посмотреть на цель, если она есть
        if (CharacterPresenter.Controller.TurnFaceToTarget(
            targetToAttack, CharacterPresenter.MyStats.rotationSpeed.GetValue(),
            CharacterPresenter.MyStats.faceEuler))
        {
            if (attackCooldown <= 0f)
            {
                CharacterStats ownerStats = CharacterPresenter.MyStats;
                DamageType damageType = CharacterPresenter.MyStats.DamageType;
                float criticalChance = CharacterPresenter.MyStats.criticalChance.GetValue();
                float criticalMultiplie = CharacterPresenter.MyStats.criticalMultiplier.GetValue();
                float attackDamage = CharacterPresenter.MyStats.attackDamage.GetValue();
                int ownerMastery = CharacterPresenter.MyStats.mastery.GetValue();
                List<IAttackModifier> attackModifiers = this.attackModifiers;


                Attacker.Attack(this, ownerStats, damageType, criticalChance, criticalMultiplie, attackDamage, ownerMastery, attackModifiers);

                attackCooldown = 1f / CharacterPresenter.MyStats.attackSpeed.GetValue(); //После атаки включить кулдаун
            }
        }
    }
}
