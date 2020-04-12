using UnityEngine;

[CreateAssetMenu(fileName = "PoisoningData", menuName = "ScriptableObjects/StatusEffectData/PoisoningData")]
public class PoisoningData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.Poisoning;
    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Negative;
}
