using System.Collections.Generic;
using UnityEngine;

public delegate void RoomIsClear();
public class StatusEffectDataContainer : Singleton<StatusEffectDataContainer>
{
    [SerializeField] private List<StatusEffectData> StatusEffectDatas = new List<StatusEffectData>();
    private Dictionary<ContainerStatusEffects, StatusEffectData> StatusEffectDictionary = new Dictionary<ContainerStatusEffects, StatusEffectData>();


    protected override void AwakeSingleton()
    {
        for (int i = 0; i < StatusEffectDatas.Count; i++)
        {
            StatusEffectDictionary.Add(StatusEffectDatas[i].StatusEffect, StatusEffectDatas[i]);
        }
    }


    public StatusEffectData GetStatusEffectData(ContainerStatusEffects statusEffectKey)
    {
        if (!StatusEffectDictionary.ContainsKey(statusEffectKey))
        {
            Debug.LogError("ContainerStatusEffects with statusEffectKey:" + statusEffectKey + " does not exist");
            return StatusEffectDictionary[ContainerStatusEffects.DefaultStatusEffectData];
        }

        return StatusEffectDictionary[statusEffectKey];
    }
}
