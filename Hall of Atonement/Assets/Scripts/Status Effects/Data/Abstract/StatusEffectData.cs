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


public enum ContainerEffectTypes
{
    Positive,
    Negative
}


public abstract class StatusEffectData : ScriptableObject
{
    [SerializeField] private Sprite StatusEffectSprite;
    public abstract ContainerStatusEffects StatusEffect { get; }
    public abstract ContainerEffectTypes StatusEffectType { get; }

    public Sprite GetStatusEffectSprite() { return StatusEffectSprite; }
}
