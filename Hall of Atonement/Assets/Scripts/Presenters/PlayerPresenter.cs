using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
//[RequireComponent(typeof(PlayerCombat))]
[RequireComponent(typeof(PlayerController))]
public class PlayerPresenter : CharacterPresenter
{
    public PlayerStats MyPlayerStats { get; private protected set; }
    public PlayerCombat PlayerCombat { get; private protected set; }

    public PlayerController PlayerController { get; private protected set; }

    public PlayerUIPresenter playerUIPresenter { get; private protected set; }


    private protected override void Awake()
    {
        base.Awake();
        MyPlayerStats = (PlayerStats)MyStats;
        PlayerCombat = (PlayerCombat)Combat;
        PlayerController = (PlayerController)Controller;
        playerUIPresenter = gameObject.GetComponent<PlayerUIPresenter>();
    }


    public override void AddStatusEffect(IStatusEffectLogic statusEffect)
    {
        base.AddStatusEffect(statusEffect);
        playerUIPresenter.StatusBar.AddStatusEffectToContaine(statusEffect);
    }


    public override void RemoveStatusEffect(IStatusEffectLogic statusEffect)
    {
        base.RemoveStatusEffect(statusEffect);
        playerUIPresenter.StatusBar.RemoveStatusEffectFromContaine(statusEffect);
    }
}
