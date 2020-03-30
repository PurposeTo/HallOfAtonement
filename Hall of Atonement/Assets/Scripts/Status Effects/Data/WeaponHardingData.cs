using UnityEngine;

[CreateAssetMenu(fileName = "WeaponHardingData", menuName = "ScriptableObjects/StatusEffectData/WeaponHardingData")]
public class WeaponHardingData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.WeaponHarding;
}
