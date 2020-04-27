using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject Player;

    private Coroutine ReLoadLvlRoutine;
    private Coroutine EnterTheHallRoutine;

    string MainMenuName = "Main Menu";
    string RoomName = "Test Room";
    string PlayerSceneName = "Player Scene";

    private int roomStage;


    public void ExitToMainMenu()
    {
        roomStage = 0;
        SceneManager.LoadSceneAsync(MainMenuName, LoadSceneMode.Single);
    }

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
        roomStage++; // +1 за пройденную комнату

        yield return SceneManager.UnloadSceneAsync(RoomName);

        yield return SceneManager.LoadSceneAsync(RoomName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerSceneName));


        ReLoadLvlRoutine = null;
    }


    private IEnumerator EnterTheHallEnumerator()
    {
        roomStage = 0; //При вхождении в Зал искупления, т.е. при начале игры сложность = 0

        yield return SceneManager.LoadSceneAsync(PlayerSceneName, LoadSceneMode.Single);
        yield return SceneManager.LoadSceneAsync(RoomName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(PlayerSceneName));

        Debug.Log("Welcome to the Hall of Atonement!");

        EnterTheHallRoutine = null;
    }
}
