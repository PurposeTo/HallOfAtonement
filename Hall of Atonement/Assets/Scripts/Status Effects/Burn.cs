using UnityEngine;

public class Burn : MonoBehaviour, IDamageLogic
{
    private UnitStats targetStats;
    private FireDamage fireDamage;
    private float damage = 5f; //возможно перенести в конструктор Burn()
    private CharacterStats ownerStats;


    public Burn()
    {
        ownerStats = null;
    }


    public Burn(CharacterStats ownerStats)
    {
        this.ownerStats = ownerStats;
    }

    public void HangStatusEffect()
    {
        fireDamage = new FireDamage(damage);
        Debug.Log(gameObject.name + ": \"I am burning!\"");
    }

    void Start()
    {
        targetStats = gameObject.GetComponent<UnitStats>();
        HangStatusEffect();
    }

    void Update()
    {
        StatusEffectDamage(targetStats, ownerStats, fireDamage);
        //если повешен скрипт Burn, то у жертвы вызвать HangStatusEffect() в течение некоторого времени
        //скрипт вешается, когда коллайдер огня или коллайдер оружия (бб)/луча, которые вызывают эффект горения сталкивается с коллайдером жертвы

        //StartCoroutine
        //WaitForSeconds
    }

    public void StatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats, FireDamage fireDamage)
    {
        targetStats.TakeDamage(ownerStats, fireDamage, fireDamage.Damage * Time.deltaTime);
    }
}
