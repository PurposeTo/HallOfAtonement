using UnityEngine;

[CreateAssetMenu(fileName = "FriendlyFireDefenceData", menuName = "ScriptableObjects/StatusEffectData/FriendlyFireDefenceData")]
public class FriendlyFireDefenceData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.FriendlyFireDefence;
}
