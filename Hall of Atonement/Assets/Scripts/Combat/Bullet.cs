using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject, IAttackModifier
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

        bulletCombat.attackModifiers.Remove(this);
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

            //Destroy(mySelf);
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

        // Внимание! Модификатор атаки пули должен быть строго в конце списка, так как он отключает пулю!
        bulletCombat.attackModifiers.Add(this);

    }


    private IEnumerator WaitingEnumerator(float waiting)
    {
        yield return new WaitForSeconds(waiting);

        gameObject.SetActive(false);

        waitingRoutine = null;
    }


    public void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery, bool isCritical = false)
    {
        gameObject.SetActive(false);
    }


    public object Clone()
    {
        return MemberwiseClone();

    }
}
