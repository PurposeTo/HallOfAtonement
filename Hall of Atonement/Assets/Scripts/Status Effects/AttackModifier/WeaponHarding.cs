using UnityEngine;

class WeaponHarding : MonoBehaviour, IAttackModifier
{
    [SerializeField] private HardingType hardingType;
    private CharacterPresenter characterPresenter;


    private void Start()
    {

        characterPresenter = gameObject.GetComponent<CharacterPresenter>();
        characterPresenter.Combat.attackModifiers.Add(this);
    }


    private void OnDestroy()
    {
        characterPresenter.Combat.attackModifiers.Remove(this);
    }


    public void SetHardingType(HardingType hardingType)
    {
        this.hardingType = hardingType;
    }


    public void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery)
    {
        if (damageType is PhysicalDamage) // Закалка применима только для физ. типа урона
        {
            switch (hardingType)
            {
                case HardingType.Burn:
                    new StatusEffectFactory<Burn>(targetStats.gameObject, characterPresenter.MyStats, mastery);
                    break;
                case HardingType.Freeze:
                    new StatusEffectFactory<Freeze>(targetStats.gameObject, characterPresenter.MyStats, mastery);
                    break;
                case HardingType.Poison:
                    new StatusEffectFactory<Poisoning>(targetStats.gameObject, characterPresenter.MyStats, mastery);
                    break;
                case HardingType.Bleeding:
                    new StatusEffectFactory<Bleeding>(targetStats.gameObject, characterPresenter.MyStats, mastery);
                    break;
                default:
                    Debug.LogError("Try to use unknown element for harding weapon!");
                    break;
            }
        }
    }


    public enum HardingType
    {
        DefaultState, // Сначала вызвать SetHardingType()
        Burn,
        Freeze,
        Poison,
        Bleeding
    }


    public object Clone()
    {
        return MemberwiseClone();
    }
}
