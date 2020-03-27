﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public Rigidbody2D bulletRb2d { get; private set; }
    private UnitCombat bulletCombat;


    private GameObject ownerGameObject;
    private CharacterStats ownerStats;
    private DamageType damageType;
    private float criticalChance;
    private float criticalMultiplie;
    private float attackDamage;
    private int ownerMastery;

    private Coroutine waitingRoutine;
    private readonly float lifeTime = 20f;

    private void Awake()
    {
        bulletRb2d = GetComponent<Rigidbody2D>();
        bulletCombat = GetComponent<UnitCombat>();
    }


    private void OnDisable()
    {
        if (waitingRoutine != null)
        {
            StopCoroutine(waitingRoutine);
            waitingRoutine = null;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //RangedGunCombat.BulletDoDamage(gameObject, collision.gameObject, combat);

        GameObject targetGameObject = collision.gameObject;

        if (targetGameObject != ownerGameObject && !collision.isTrigger)
        {
            Debug.Log("Пуля " + ownerGameObject + "попала в: " + targetGameObject);

            bool isEvade = false;

            //Это что то имеет Статы?
            if (targetGameObject.TryGetComponent(out UnitStats targetStats))
            {
                bulletCombat.DoDamage(targetStats, ownerStats, damageType, criticalChance, criticalMultiplie, attackDamage, ownerMastery, bulletCombat.attackModifiers);
            }

            // Разбить, только если цель НЕ увернулась
            if (!isEvade)
            {
                gameObject.SetActive(false);

            }
        }
    }


    void IPooledObject.OnObjectSpawn()
    {
        if(waitingRoutine == null) 
        { 
            waitingRoutine = StartCoroutine(WaitingEnumerator(lifeTime));
        }
    }


    public void BulletInitialization(GameObject ownerGameObject, CharacterStats ownerStats, DamageType damageType, float criticalChance, float criticalMultiplie, float attackDamage, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        this.ownerGameObject = ownerGameObject;
        this.ownerStats = ownerStats;
        this.damageType = (DamageType)damageType.Clone();
        this.criticalChance = criticalChance;
        this.criticalMultiplie = criticalMultiplie;
        this.attackDamage = attackDamage;
        this.ownerMastery = ownerMastery;

        bulletCombat.attackModifiers = (List<IAttackModifier>)GameLogic.Clone(attackModifiers);
    }


    private IEnumerator WaitingEnumerator(float waiting)
    {
        yield return new WaitForSeconds(waiting);

        gameObject.SetActive(false);

        waitingRoutine = null;
    }
}
