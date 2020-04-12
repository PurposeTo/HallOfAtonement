using UnityEngine;
using System.Collections.Generic;

public abstract class CharacterCombat : MonoBehaviour
{
    public CharacterPresenter CharacterPresenter { get; private protected set; }

    private protected GameObject targetToAttack = null;

    public DamageUnit DamageUnit { get; private set; } = new DamageUnit();

    private Cooldown attackCooldown;

    public IWeapon Weapon { get; private protected set; }

    public List<IAttackModifier> attackModifiers = new List<IAttackModifier>();

    private protected virtual void Start()
    {
        CharacterPresenter = GetComponent<CharacterPresenter>();

        Weapon = GetComponent<IWeapon>();

        attackCooldown = gameObject.AddComponent<Cooldown>();
    }


    //private protected virtual void Update()
    //{
    //    if (attackCooldown > 0f) //Если кулдаун больше нуля, то уменьшить
    //    {
    //        attackCooldown -= Time.deltaTime;
    //    }
    //    else if (targetToAttack == null && attackCooldown < 0f) //Если игрок сейчас НЕ атакует и кулдаун меньше нуля, то сбросить кулдаун
    //    {
    //        attackCooldown = 0f;
    //    }
    //}


    public GameObject GetTargetToAttack() { return targetToAttack; }


    public void SetTargetToAttack(GameObject targetToAttack) { this.targetToAttack = targetToAttack; }


    private protected void Attack(GameObject target)
    {
        targetToAttack = target;

        //Посмотреть на цель, если она есть
        if (CharacterPresenter.CharacterMovement.TurnFaceToTarget(
            targetToAttack, CharacterPresenter.MyStats.rotationSpeed.GetValue(),
            CharacterPresenter.MyStats.faceEuler))
        {
            if (attackCooldown.IsReady())
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

                Weapon.UseWeapon(this, ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);

            }
        }
    }


    public void EnableAttackCooldown() // Данный метод вызывается напрямую из оружия!
    {
        attackCooldown.SetCooldownTimeAndStart(1f / CharacterPresenter.MyStats.attackSpeed.GetValue());
    }
}
