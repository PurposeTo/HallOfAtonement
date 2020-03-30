using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStatusEffectData", menuName = "ScriptableObjects/StatusEffectData/DefaultStatusEffectData")]
public class DefaultStatusEffectData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.DefaultStatusEffectData;
}
