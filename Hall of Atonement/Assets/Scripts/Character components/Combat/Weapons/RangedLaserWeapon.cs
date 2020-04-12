﻿using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class RangedLaserWeapon : MonoBehaviour, IRanged
{
    public Transform weapon;

    private Ray2D ray;

    Transform IWeapon.AttackPoint => weapon;

    private Coroutine casteRoutine;

    private float delayBeforeUseWeapon = 0.5f;


    void IWeapon.UseWeapon(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        if(casteRoutine == null)
        {
            casteRoutine = StartCoroutine(CasteEnumerator(combat, ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers));
        }
    }


    private IEnumerator CasteEnumerator(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        combat.CharacterPresenter.CharacterMovement.DisableMovement();

        //yield return new WaitForSeconds(0.5f);

        for (float currentCount = delayBeforeUseWeapon; currentCount > 0f; currentCount -= Time.deltaTime)
        {
            yield return null;
        }

        Shoot(combat, ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);

        combat.CharacterPresenter.CharacterMovement.EnableMovement();
        combat.EnableAttackCooldown();
        casteRoutine = null;
    }


    private void Shoot(CharacterCombat combat, CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        //ТУТ надо запустить корутину ожидания "Накопление энергии", в процессе которой носитель данного оружия будет стоять...
        // Так же необходимо срывать каст по желанию...

        print(gameObject.name + " использует дальнюю атаку!");

        RaycastHit2D hit;

        Quaternion rotation = transform.rotation;
        ray = new Ray2D(weapon.position, rotation * Vector2.up);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.yellow, 0.6f);

        //Выстрелить
        hit = Physics2D.Raycast(ray.origin, ray.direction);

        //Лазер попала во что то
        //for (int i = 0; i < hit.Length; i++)
        //{
        if (hit.collider.gameObject != gameObject)
        {
            Debug.Log(gameObject.name + " попал в: " + hit.collider.gameObject.name);

            //Это что то имеет Статы?
            if (hit.collider.gameObject.TryGetComponent(out UnitStats targetStats))
            {
                combat.DamageUnit.DoDamage(targetStats, ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);
            }
        }

        //}



    }
}
