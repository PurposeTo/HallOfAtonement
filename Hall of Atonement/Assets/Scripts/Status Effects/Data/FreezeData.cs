using UnityEngine;

[CreateAssetMenu(fileName = "FreezeData", menuName = "ScriptableObjects/StatusEffectData/FreezeData")]
public class FreezeData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.Freeze;
    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Negative;
}
