using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
//[RequireComponent(typeof(CharacterCombat))]
[RequireComponent(typeof(CharacterMovement))]
public class CharacterPresenter : UnitPresenter
{
    public CharacterStats MyStats { get; private protected set; }
    public CharacterCombat Combat { get; private protected set; }

    public CharacterMovement Controller { get; private protected set; }

    public CharacterType CharacterType { get; private protected set; }




    private protected override void Awake()
    {
        base.Awake();

        MyStats = (CharacterStats)UnitStats;
        Combat = GetComponent<CharacterCombat>();
        Controller = GetComponent<CharacterMovement>();
        CharacterType = GetComponent<CharacterType>();
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
