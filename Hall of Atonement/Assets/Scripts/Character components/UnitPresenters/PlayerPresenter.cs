using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
//[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerPresenter : CharacterPresenter
{
    public PlayerStats MyPlayerStats { get; private protected set; }
    public PlayerCombat PlayerCombat { get; private protected set; }

    public PlayerMovement PlayerController { get; private protected set; }

    public PlayerUIPresenter PlayerUIPresenter;


    private protected override void Awake()
    {
        base.Awake();
        MyPlayerStats = (PlayerStats)MyStats;
        PlayerCombat = (PlayerCombat)Combat;
        PlayerController = (PlayerMovement)Controller;
    }


    public override void AddStatusEffect(IStatusEffectLogic statusEffect)
    {
        base.AddStatusEffect(statusEffect);
        PlayerUIPresenter.StatusBar.AddStatusEffectToContaine(statusEffect);
    }


    public override void RemoveStatusEffect(IStatusEffectLogic statusEffect)
    {
        base.RemoveStatusEffect(statusEffect);
        PlayerUIPresenter.StatusBar.RemoveStatusEffectFromContaine(statusEffect);
    }
}
