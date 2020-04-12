using UnityEngine;

[CreateAssetMenu(fileName = "FuryBladesData", menuName = "ScriptableObjects/StatusEffectData/FuryBladesData")]
public class FuryBladesData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.FuryBlades;
    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Positive;
}
