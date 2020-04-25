using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoomController : Singleton<RoomController>
{
    public event RoomIsClear OnRoomIsClear;

    private List<GameObject> enemys = new List<GameObject>();

    private float startDelay = 1f;


    private IEnumerator Start()
    {
        for (float counter = startDelay; counter > 0; counter -= Time.deltaTime)
        {
            yield return null;
        }

        CheckRoom();
    }


    public void AddEnemyToAllEnemysList(GameObject enemy) { enemys.Add(enemy); }
    public void RemoveEnemyFromAllEnemysList(GameObject enemy)
    {
        enemys.Remove(enemy);

        CheckRoom();
    }


    private void CheckRoom()
    {
        if (enemys.Count == 0)
        {
            OnRoomIsClear?.Invoke();
        }
    }
}
