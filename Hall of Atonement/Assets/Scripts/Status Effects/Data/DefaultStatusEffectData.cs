using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStatusEffectData", menuName = "ScriptableObjects/StatusEffectData/DefaultStatusEffectData")]
public class DefaultStatusEffectData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.DefaultStatusEffectData;

    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Negative;
}
