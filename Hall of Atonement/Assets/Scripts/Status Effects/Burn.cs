using UnityEngine;

public class Burn : MonoBehaviour, IDamageLogic
{
    private UnitStats targetStats;
    private CharacterStats ownerStats = null;
    private DamageType damageType;

    private readonly float baseDamagePerSecond = 2f;  //возможно перенести в конструктор Burn()
    private readonly float baseBurningTime = 3f;

    private float currentDamagePerSecond;
    private float currentBurningTime;


    //4. Растопить лед


    void Start()
    {
        damageType = new FireDamage();
        Debug.Log(gameObject.name + ": \"I am burning!\"");
        targetStats = gameObject.GetComponent<UnitStats>();
    }


    void Update()
    {
        StatusEffectDamage(targetStats, ownerStats, damageType);
    }


    public void StatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats, DamageType fireDamage)
    {
        if(currentBurningTime > 0f)
        {
            targetStats.TakeDamage(ownerStats, fireDamage, currentDamagePerSecond * Time.deltaTime, out bool _);
            currentBurningTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this);
        }
    }


    public void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        this.ownerStats = ownerStats;
        currentBurningTime = baseBurningTime;
        currentDamagePerSecond += baseDamagePerSecond * amplificationAmount;
    }
}
