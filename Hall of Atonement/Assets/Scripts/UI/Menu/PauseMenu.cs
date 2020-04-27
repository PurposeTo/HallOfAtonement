using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject PauseMenuCanvas;

    private bool pause = false;

    public void TogglePause()
    {
        pause = !pause;
        SetTime();
    }


    private void SetTime()
    {
        if (pause)
        {
            PauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            PauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
