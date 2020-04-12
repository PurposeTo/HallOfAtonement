using UnityEngine;

[CreateAssetMenu(fileName = "BleedingData", menuName = "ScriptableObjects/StatusEffectData/BleedingData")]
public class BleedingData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.Bleeding;
    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Negative;
}
