using UnityEngine;

class WeaponHarding<T> : MonoBehaviour, IAttackModifier where T : ItemHarding
{
    public HardingType hardingType;
    private T debaff;
    private UnitStats targetStats;
    private CharacterStats ownerStats;
    private DamageTypeEffect statusEffectFactory = new DamageTypeEffect();
    private HardingFactory<T> hardingFactory = new HardingFactory<T>();


    void Start()
    {
        Initialization();
    }


    void Update()
    {
        
    }


    private void Initialization()
    {
        switch (hardingType)
        {
            case HardingType.Burn:
                debaff = new Burn();
                statusEffectFactory.HangDamageTypeEffect(new FireDamage(), targetStats, ownerStats, 1f);
                break;
            case HardingType.Freeze:
                break;
            case HardingType.Poison:
                break;
            case HardingType.Bleeding:
                break;
            default:
                Debug.LogError("Try to use unknown element for harding weapon!");
                break;
        }
    }


    public void ApplyAttackModifier(float damage, int mastery)
    {
        hardingFactory.Foo(debaff, targetStats, ownerStats);
    }

    public object Clone()
    {
        throw new System.NotImplementedException();
    }

    public enum HardingType
    {
        Burn,
        Freeze,
        Poison,
        Bleeding
    }
}
