using UnityEngine;

[CreateAssetMenu(fileName = "FriendlyFireDefenceData", menuName = "ScriptableObjects/StatusEffectData/FriendlyFireDefenceData")]
public class FriendlyFireDefenceData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.FriendlyFireDefence;
    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Positive;
}
