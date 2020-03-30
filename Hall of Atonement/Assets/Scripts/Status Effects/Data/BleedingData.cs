using UnityEngine;

[CreateAssetMenu(fileName = "BleedingData", menuName = "ScriptableObjects/StatusEffectData/BleedingData")]
public class BleedingData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Bleeding;
}
