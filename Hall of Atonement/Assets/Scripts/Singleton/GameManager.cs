using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : Singleton<GameManager>
{
    public event LevelIsClear OnLevelIsClear;

    public GameObject player;

    private List<GameObject> enemys = new List<GameObject>();


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
