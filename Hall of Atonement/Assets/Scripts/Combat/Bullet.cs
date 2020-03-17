﻿using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour//, IPooledObject
{
    public Rigidbody2D bulletRb2d { get; private set; }
    private UnitCombat bulletCombat;


    GameObject ownerGameObject;
    CharacterStats ownerStats;
    DamageType damageType;
    float criticalChance;
    float criticalMultiplie;
    float attackDamage;
    int ownerMastery;



    private void Awake()
    {
        bulletRb2d = GetComponent<Rigidbody2D>();
        bulletCombat = GetComponent<UnitCombat>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        //RangedGunCombat.BulletDoDamage(gameObject, collision.gameObject, combat);

        GameObject targetGameObject = collision.gameObject;

        if (targetGameObject != ownerGameObject && !collision.isTrigger)
        {
            Debug.Log("Пуля " + ownerGameObject + "попала в: " + targetGameObject);


            //Это что то имеет Статы?
            if (targetGameObject.TryGetComponent(out UnitStats targetStats))
            {
                bulletCombat.DoDamage(targetStats, ownerStats, damageType, criticalChance, criticalMultiplie, attackDamage, ownerMastery, bulletCombat.attackModifiers);
            }

            gameObject.SetActive(false);
            //Destroy(mySelf);
        }
    }


    //void IPooledObject.OnObjectSpawn()
    //{

    //}


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
}