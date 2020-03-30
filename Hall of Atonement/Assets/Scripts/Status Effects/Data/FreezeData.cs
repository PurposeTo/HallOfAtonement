using UnityEngine;

[CreateAssetMenu(fileName = "FreezeData", menuName = "ScriptableObjects/StatusEffectData/FreezeData")]
public class FreezeData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Freeze;
}
