using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterMovement))]
public abstract class CharacterCombat : MonoBehaviour
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }

    //[HideInInspector] 
    public GameObject targetToAttack = null;

    public DamageUnit DamageUnit { get; private set; } = new DamageUnit();
    private float attackCooldown;

    public IWeapon Attacker { get; private protected set; }

    public List<IAttackModifier> attackModifiers = new List<IAttackModifier>();

    private protected virtual void Start()
    {
        CharacterPresenter = GetComponent<CharacterPresenter>();

        Attacker = GetComponent<IWeapon>();
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

                bool isCritical = false;
                //Если Крит. шанс больше нуля И если рандом говорит о том, что нужно критануть
                if (criticalChance > 0f && Random.Range(0f, 1f) <= criticalChance)
                {
                    attackDamage *= criticalMultiplie; //То увеличить урон
                    isCritical = true;
                }

                Attacker.Attack(this, ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);

                attackCooldown = 1f / CharacterPresenter.MyStats.attackSpeed.GetValue(); //После атаки включить кулдаун
            }
        }
    }
}
