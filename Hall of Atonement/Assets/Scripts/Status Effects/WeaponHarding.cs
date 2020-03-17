using UnityEngine;

class WeaponHarding : MonoBehaviour, IAttackModifier
{
    public HardingType hardingType;
    private ItemHarding debaff;
    private UnitStats targetStats;
    private StatusEffectFactory statusEffectFactory = new StatusEffectFactory();


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

    }

    public enum HardingType
    {
        Burn,
        Freeze,
        Poison,
        Bleeding
    }
}
