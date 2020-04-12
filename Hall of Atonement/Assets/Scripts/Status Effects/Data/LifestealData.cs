using UnityEngine;

[CreateAssetMenu(fileName = "LifestealData", menuName = "ScriptableObjects/StatusEffectData/LifestealData")]
public class LifestealData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.Lifesteal;

    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Positive;
}
