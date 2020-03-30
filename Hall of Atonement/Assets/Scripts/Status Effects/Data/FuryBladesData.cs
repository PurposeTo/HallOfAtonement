using UnityEngine;

[CreateAssetMenu(fileName = "FuryBladesData", menuName = "ScriptableObjects/StatusEffectData/FuryBladesData")]
public class FuryBladesData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.FuryBlades;
}
