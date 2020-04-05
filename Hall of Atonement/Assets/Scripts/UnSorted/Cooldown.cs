using UnityEngine;

public class Cooldown
{
    private bool isReady = false;


    public bool IsReady()
    {
        return isReady;
    }


    public void SetCooldownTimeAndStart(float cooldownTime)
    {
        if (isReady)
        {
            // Начинаем отсчет

            isReady = false;
        }
        else
        {
            Debug.Log("Not ready!");
        }
    }
}
