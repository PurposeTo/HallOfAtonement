using UnityEngine;

public enum ContainerStatusEffects
{
    DefaultStatusEffectData,

    Burn,
    Freeze,
    Bleeding,
    Poisoning,

    Lifesteal,
    FuryBlades,
    WeaponHarding,

    FriendlyFireDefence
}
public abstract class StatusEffectData : ScriptableObject
{
    public Sprite StatusEffectSprite;
    //public StatusEffect StatusEffect; Not Working!

    public abstract ContainerStatusEffects StatusEffectType { get; }

}
