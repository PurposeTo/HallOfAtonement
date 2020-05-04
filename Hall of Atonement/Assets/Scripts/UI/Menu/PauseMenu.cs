using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject playerControllers;

    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject pauseButton;

    private bool pause = false;

    public void SetPause()
    {
        pause = true;
        SetTime();
        pauseMenu.SetActive(true);

        pauseButton.SetActive(false);
        playerControllers.SetActive(false);
    }



    public void Resume()
    {
        pause = false;
        SetTime();
        pauseMenu.SetActive(false);

        pauseButton.SetActive(true);
        playerControllers.SetActive(true);
    }


    public void ExitToMainMenu()
    {
        // Вынести потом отключение паузы в гейм мннеджер, так как она должна отключаться уже ПОСЛЕ выхода в меню
        pause = false;
        SetTime();
        GameManager.Instance.ExitToMainMenu();
    }


    private void SetTime()
    {
        if (pause)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
