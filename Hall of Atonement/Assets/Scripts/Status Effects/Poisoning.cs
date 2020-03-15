using UnityEngine;

class Poisoning : MonoBehaviour, IDamageLogic
{
    private UnitStats targetStats;
    private CharacterStats ownerStats;
    private DamageType damageType;

    private readonly float baseDamagePerSecond = 2f;
    private readonly float basePoisoningTime = 4f;
    private readonly float effectTimeUpdating = 1.5f;

    private float currentPoisoningTime;
    private float effectPower;


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
        damageType = new PoisonDamage();
        targetStats = gameObject.GetComponent<UnitStats>();
        Debug.Log(gameObject.name + @": ""I was poisoned!""");
    }


    public void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        this.ownerStats = ownerStats;
        currentPoisoningTime = basePoisoningTime * effectTimeUpdating; //обновили и увеличили на (1/2)
        effectPower += amplificationAmount;
    }


    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats ownerStats)
    {
        if (currentPoisoningTime > 0f)
        {
            targetStats.TakeDamage(ownerStats, damageType, baseDamagePerSecond * effectPower * Time.deltaTime, false, out bool _, out bool _);
            currentPoisoningTime -= Time.deltaTime;
        }
        else
        {
            Destroy(this);
        }
    }
}
