using UnityEngine;

[CreateAssetMenu(fileName = "BurnData", menuName = "ScriptableObjects/StatusEffectData/BurnData")]
public class BurnData : StatusEffectData
{
    public override ContainerStatusEffects StatusEffectType { get; } = ContainerStatusEffects.Burn;
}
