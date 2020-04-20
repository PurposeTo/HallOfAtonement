using System.Collections.Generic;
using UnityEngine;


public class RoomController : Singleton<RoomController>
{
    public event RoomIsClear OnRoomIsClear;

    public GameObject player;

    private List<GameObject> enemys = new List<GameObject>();


    public void AddEnemyToAllEnemysList(GameObject enemy) { enemys.Add(enemy); }
    public void RemoveEnemyFromAllEnemysList(GameObject enemy)
    {
        enemys.Remove(enemy);

        if (enemys.Count == 0)
        {
            OnRoomIsClear?.Invoke();
        }
    }
}
