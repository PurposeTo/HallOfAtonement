using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour//, IPooledObject
{
    public Rigidbody2D bulletRb2d { get; private set; }

    private GameObject ownerGameObject;
    private CharacterStats ownerStats;

    private DamageType damageType;
    private float bulletDamage;
    private int ownerMastery;
    private List<IAttackModifier> attackModifiers;



    private void Awake()
    {
        bulletRb2d = GetComponent<Rigidbody2D>();
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
                BulletDoDamage(targetStats);
            }

            gameObject.SetActive(false);
            //Destroy(mySelf);
        }
    }


    //void IPooledObject.OnObjectSpawn()
    //{

    //}


    public void BulletInitialization(GameObject ownerGameObject, CharacterStats ownerStats, CharacterCombat characterCombat)
    {
        this.ownerGameObject = ownerGameObject;
        this.ownerStats = ownerStats;
        this.damageType = (DamageType)ownerStats.DamageType.Clone();

        float bulletDamage = ownerStats.attackDamage.GetValue();

        if (ownerStats.criticalChance.GetValue() > 0f && Random.Range(1f, 100f) <= ownerStats.criticalChance.GetValue())
        {
            bulletDamage *= ownerStats.criticalMultiplier.GetValue(); //То увеличить урон
        }

        this.bulletDamage = bulletDamage;
        this.ownerMastery = ownerStats.mastery.GetValue();
        this.attackModifiers = characterCombat.attackModifiers;
    }


    private void BulletDoDamage(UnitStats targetStats)
    {
        bool isEvaded = false;
        bool isBlocked = false;

        bulletDamage = targetStats.TakeDamage(ownerStats, damageType, bulletDamage, true, ref isEvaded, ref isBlocked);

        if (!isEvaded)
        {
            if (!isBlocked) //Если урон не был полностью заблокирован 
            {
                if (damageType is EffectDamage)
                {
                    StatusEffectFactory statusEffectFactory = new StatusEffectFactory();
                    statusEffectFactory.HangStatusEffect(damageType, targetStats, ownerStats);
                }
            }


            /*
             * Есть ли модификатор атаки?
             * 
             * Если у цели есть модификатор атаки, который должен навесить дебафф, то сам модификатор проверяет, может ли он это сделать.
             */



            for (int i = 0; i < attackModifiers.Count; i++)
            {
                attackModifiers[i].ApplyAttackModifier(bulletDamage, ownerMastery);
            }
        }
    }
}
