﻿public class PlayerPresenter : CharacterPresenter
{
    public PlayerStats MyPlayerStats { get; private protected set; }
    public PlayerCombat PlayerCombat { get; private protected set; }

    public PlayerUIPresenter PlayerUIPresenter;


    private protected override void Awake()
    {
        base.Awake();
        MyPlayerStats = (PlayerStats)MyStats;
        PlayerCombat = (PlayerCombat)Combat;
    }


    public override void AddStatusEffect(StatusEffect statusEffect)
    {
        base.AddStatusEffect(statusEffect);
        PlayerUIPresenter.StatusBar.AddStatusEffectToContaine(statusEffect);
    }


    public override void RemoveStatusEffect(StatusEffect statusEffect)
    {
        base.RemoveStatusEffect(statusEffect);
        PlayerUIPresenter.StatusBar.RemoveStatusEffectFromContaine(statusEffect);
    }
}
