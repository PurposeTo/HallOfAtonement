using UnityEngine;

[CreateAssetMenu(fileName = "WeaponHardingData", menuName = "ScriptableObjects/StatusEffectData/WeaponHardingData")]
public class WeaponHardingData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffect { get; } = ContainerStatusEffects.WeaponHarding;
    public override ContainerEffectTypes StatusEffectType => ContainerEffectTypes.Positive;
}
