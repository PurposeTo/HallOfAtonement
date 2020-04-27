using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    public Rigidbody2D bulletRb2d { get; private set; }
    private readonly DamageUnit damageUnit = new DamageUnit();

    private CharacterStats ownerStats;
    private DamageType damageType;
    private float attackDamage;
    private bool isCritical;
    private int ownerMastery;
    List<IAttackModifier> attackModifiers;

    private Coroutine waitingRoutine;
    private readonly float lifeTime = 20f;

    private void Awake()
    {
        bulletRb2d = GetComponent<Rigidbody2D>();
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

        if (!collision.isTrigger)
        {
            //Это что то имеет Статы?
            collision.gameObject.TryGetComponent(out UnitStats targetStats);


            if (targetStats != ownerStats) // Если мы попали не в себя
            {
                Debug.Log("Пуля " + ownerStats + " попала в: " + collision.gameObject);


                if (targetStats != null)
                {
                    damageUnit.DoDamage(targetStats, ownerStats, damageType, attackDamage, isCritical, ownerMastery, attackModifiers);
                }


                bool isEvade = false;

                // Разбить, только если цель НЕ увернулась
                if (!isEvade)
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }


    void IPooledObject.OnObjectSpawn()
    {
        if (waitingRoutine == null)
        {
            waitingRoutine = StartCoroutine(WaitingEnumerator(lifeTime));
        }
    }


    public void BulletInitialization(CharacterStats ownerStats, DamageType damageType, float attackDamage, bool isCritical, int ownerMastery, List<IAttackModifier> attackModifiers)
    {
        this.ownerStats = ownerStats;
        this.damageType = (DamageType)damageType.Clone();
        this.attackDamage = attackDamage;
        this.isCritical = isCritical;
        this.ownerMastery = ownerMastery;

        this.attackModifiers = (List<IAttackModifier>)GameLogic.Clone(attackModifiers);
    }


    private IEnumerator WaitingEnumerator(float waiting)
    {
        yield return new WaitForSeconds(waiting);

        gameObject.SetActive(false);

        waitingRoutine = null;
    }
}
