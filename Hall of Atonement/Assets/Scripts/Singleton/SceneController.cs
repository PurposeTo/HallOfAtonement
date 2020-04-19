using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    private Coroutine ReLoadLvlRoutine;

    string sceneName = "Test Room";


    public void ReLoadRoom()
    {
        if (ReLoadLvlRoutine == null)
        {
            ReLoadLvlRoutine = StartCoroutine(ReLoadRoomEnumerator());
        }
    }


    private IEnumerator ReLoadRoomEnumerator()
    {
        yield return SceneManager.UnloadSceneAsync(sceneName);

        yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));


        ReLoadLvlRoutine = null;
    }
}
