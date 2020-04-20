using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    private Coroutine ReLoadLvlRoutine;
    private Coroutine EnterTheHallRoutine;

    string MainMenuName = "Main Menu";
    string RoomName = "Test Room";
    string PlayerSceneName = "Player Scene";


    public void EnterTheHall()
    {
        if (EnterTheHallRoutine == null)
        {
            EnterTheHallRoutine = StartCoroutine(EnterTheHallEnumerator());
        }
    }


    public void ReLoadRoom()
    {
        if (ReLoadLvlRoutine == null)
        {
            ReLoadLvlRoutine = StartCoroutine(ReLoadRoomEnumerator());
        }
    }


    private IEnumerator ReLoadRoomEnumerator()
    {
        yield return SceneManager.UnloadSceneAsync(RoomName);

        yield return SceneManager.LoadSceneAsync(RoomName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerSceneName));


        ReLoadLvlRoutine = null;
    }


    private IEnumerator EnterTheHallEnumerator()
    {
        yield return SceneManager.LoadSceneAsync(PlayerSceneName, LoadSceneMode.Single);
        yield return SceneManager.LoadSceneAsync(RoomName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerSceneName));

        EnterTheHallRoutine = null;
    }
}
