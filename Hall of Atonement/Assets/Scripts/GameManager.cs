using System.Collections.Generic;
using UnityEngine;

public delegate void LevelIsClear();
public class GameManager : MonoBehaviour
{
    public event LevelIsClear OnLevelIsClear;

    public List<StatusEffectData> StatusEffectDatas = new List<StatusEffectData>();
    private Dictionary<ContainerStatusEffects, StatusEffectData> StatusEffectDictionary = new Dictionary<ContainerStatusEffects, StatusEffectData>();

    public static GameManager instance;

    public GameObject player;

    private List<GameObject> enemys = new List<GameObject>();

    private void Awake() //делаем объект синглтоном
    {
        if (instance == null)
        {
            instance = this;
        }
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);

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
            return null;
        }

        return StatusEffectDictionary[statusEffectKey];
    }


    public void AddEnemyToAllEnemysList(GameObject enemy) { enemys.Add(enemy); }
    public void RemoveEnemyFromAllEnemysList(GameObject enemy) 
    { 
        enemys.Remove(enemy);

        if (enemys.Count == 0)
        {
            OnLevelIsClear?.Invoke();
        }
    }
}
