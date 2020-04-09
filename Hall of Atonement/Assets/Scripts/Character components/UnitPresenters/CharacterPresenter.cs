using UnityEngine;

public class CharacterPresenter : UnitPresenter
{
    public CharacterStats MyStats { get; private protected set; }
    public CharacterCombat Combat { get; private protected set; }

    public CharacterMovement CharacterMovement { get; private protected set; }

    public CharacterType CharacterType { get; private protected set; }

    public Rigidbody2D Rb2d { get; private protected set; }





    private protected override void Awake()
    {
        base.Awake();

        MyStats = (CharacterStats)UnitStats;
        Combat = GetComponent<CharacterCombat>();
        CharacterMovement = GetComponent<CharacterMovement>();
        CharacterType = GetComponent<CharacterType>();
        Rb2d = GetComponent<Rigidbody2D>();
    }


    public override void AddStatusEffect(IStatusEffectLogic statusEffect)
    {
        base.AddStatusEffect(statusEffect);

        if(statusEffect is IAttackModifier)
        {
            Combat.attackModifiers.Add((IAttackModifier)statusEffect);
        }
        else if (statusEffect is IDefenseModifier)
        {
            MyStats.defenseModifiers.Add((IDefenseModifier)statusEffect);
        }
    }


    public override void RemoveStatusEffect(IStatusEffectLogic statusEffect)
    {
        base.RemoveStatusEffect(statusEffect);

        if (statusEffect is IAttackModifier)
        {
            Combat.attackModifiers.Remove((IAttackModifier)statusEffect);
        }
        else if (statusEffect is IDefenseModifier)
        {
            MyStats.defenseModifiers.Remove((IDefenseModifier)statusEffect);
        }
    }
}
