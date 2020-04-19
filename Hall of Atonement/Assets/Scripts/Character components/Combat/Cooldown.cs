using System.Collections;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    private bool isReady = true;
    private float currentCooldownTime;

    private Coroutine counter;

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

            if (counter == null)
            {
                counter = StartCoroutine(EnumeratorCounter(cooldownTime));
            }
            else
            {
                Debug.LogWarning("Something wrong with cooldown counter");
            }
        }
        else
        {
            Debug.Log("Not ready!");
        }
    }


    private IEnumerator EnumeratorCounter(float cooldownTime)
    {
        currentCooldownTime = cooldownTime; // Стоит учитывать максимальное время?

        while (currentCooldownTime > 0)
        {
            yield return null;
            currentCooldownTime -= Time.deltaTime;
        }

        currentCooldownTime = 0f;
        isReady = true;
        counter = null;
    }
}
