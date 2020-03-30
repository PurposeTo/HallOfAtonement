using UnityEngine;

[CreateAssetMenu(fileName = "PoisoningData", menuName = "ScriptableObjects/StatusEffectData/PoisoningData")]
public class PoisoningData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Poisoning;
}
