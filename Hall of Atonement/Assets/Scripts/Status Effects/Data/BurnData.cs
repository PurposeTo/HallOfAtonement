using UnityEngine;

[CreateAssetMenu(fileName = "BurnData", menuName = "ScriptableObjects/StatusEffectData/BurnData")]
public class BurnData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.Burn;

    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Negative;
}
