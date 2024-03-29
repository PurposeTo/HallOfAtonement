﻿using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    public EnemyPresenter EnemyPresenter { get; private protected set; }
    public IEnemyAttackBehavior EnemyAttackBehavior { get; private protected set; }


    private protected override void Start()
    {
        base.Start();
        EnemyPresenter = (EnemyPresenter)CharacterPresenter;

        // Проверить, какое оружие имеет данный враг и дать ему соответствующую логику
        if (Weapon is IMelee)
        {
            EnemyAttackBehavior = gameObject.AddComponent<MeleeEnemyBehaviour>();
        }
        else if (Weapon is IRanged) 
        {
            EnemyAttackBehavior = gameObject.AddComponent<RangedEnemyBehaviour>();
        }
    }


    public void GetEnemyFightingLogic(GameObject focusTarget)
    {
        float distance = Vector2.Distance(focusTarget.transform.position, transform.position);
        float focusTargetScale = focusTarget.transform.localScale.x;

        // Как/когда нужно атаковать?
        if (distance - (focusTargetScale / 2f) <= EnemyPresenter.EnemyCombat.EnemyAttackBehavior.GetAttackRange())
        {
            Attack(focusTarget);
        }
        else
        {
            // Если TargetToAttack не в зоне поражения оружия, то стоит обнулить его
            targetToAttack = null;

        }
    }
}
