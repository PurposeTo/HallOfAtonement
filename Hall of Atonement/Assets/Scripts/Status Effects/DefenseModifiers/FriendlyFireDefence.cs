using UnityEngine;

public class FriendlyFireDefence : StatusEffect, IDefenseModifier
{
    private protected override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.FriendlyFireDefence;

    public override StatusEffectData StatusEffectData => StatusEffectDataContainer.Instance.GetStatusEffectData(StatusEffectType);


    private CharacterPresenter characterPresenter;

    void IDefenseModifier.ApplyDefenseModifier(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked)
    {
        if (killerStats.CharacterPresenter.CharacterType.GetType() == characterPresenter.CharacterType.GetType())
        {
            isEvaded = true;
        }
    }

    private void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();

        characterPresenter.AddStatusEffect(this);
    }


    private void OnDestroy()
    {
        characterPresenter.RemoveStatusEffect(this);
    }
}
