using UnityEngine;

[CreateAssetMenu(fileName = "LifestealData", menuName = "ScriptableObjects/StatusEffectData/LifestealData")]
public class LifestealData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Lifesteal;
}
