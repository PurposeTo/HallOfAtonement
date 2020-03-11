using UnityEngine;

public class Burn : MonoBehaviour, IDamageLogic
{
    private UnitStats targetStats;
    private CharacterStats ownerStats = null;
    private DamageType damageType;

    private readonly float baseDamagePerSecond = 2f;
    private readonly float baseBurningTime = 3f;

    private float effectPower;
    private float currentBurningTime;


    void Start()
    {
        Initialization();
    }


    void Update()
    {
        DoStatusEffectDamage(targetStats, ownerStats);
    }


    private void Initialization()
    {
        damageType = new FireDamage();
        Debug.Log(gameObject.name + ": \"I am burning!\"");
        targetStats = gameObject.GetComponent<UnitStats>();

        //Проверить, есть ли на цели лед. Если есть, то разморозить.
        if (targetStats.gameObject.TryGetComponent(out Freeze freeze))
        {
            freeze.SelfDestruction();
        }
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats)
    {
        if(currentBurningTime > 0f)
        {
            targetStats.TakeDamage(ownerStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, false, out bool _, out bool _);
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
        effectPower += amplificationAmount;
    }


    public void SelfDestruction()
    {
        float remainingDamage = baseDamagePerSecond * effectPower * currentBurningTime;
        targetStats.TakeDamage(ownerStats, damageType, remainingDamage, false, out bool _, out bool _);
        Destroy(this);
    }
}
