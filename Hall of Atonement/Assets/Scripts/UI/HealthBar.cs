using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider HealthSlider;
    public Slider HealthChangeSlider;

    private float maxSliderValue = 100f;

    private float speedHealthChange = 15f;

    private float delayBeforeChange = 0.3f;

    private Coroutine RoutineChangeHealth = null;


    public void Start()
    {
        HealthSlider.maxValue = maxSliderValue;
        HealthSlider.value = maxSliderValue;
        HealthChangeSlider.maxValue = maxSliderValue;
        HealthChangeSlider.value = maxSliderValue;

    }


    public void DecreaseHealthBar(UnitStats myStats)
    {
        HealthSlider.value = myStats.CurrentHealthPoint / myStats.maxHealthPoint.GetValue() * maxSliderValue;

        if (RoutineChangeHealth == null)
        {
            RoutineChangeHealth = StartCoroutine(ChangeHealthFill());
        }
    }


    public void IncreaseHealthBar(UnitStats myStats)
    {
        HealthSlider.value = myStats.CurrentHealthPoint / myStats.maxHealthPoint.GetValue() * maxSliderValue;

        if (RoutineChangeHealth == null)
        {
            HealthChangeSlider.value = HealthChangeSlider.value;
        }
    }

    private IEnumerator ChangeHealthFill()
    {
        yield return new WaitForSeconds(delayBeforeChange);

        while (true)
        {
            if (HealthChangeSlider.value < HealthSlider.value)
            {
                HealthChangeSlider.value = HealthChangeSlider.value;
                break;
            }
            else if (HealthChangeSlider.value > HealthSlider.value)
            {
                HealthChangeSlider.value = Mathf.MoveTowards(HealthChangeSlider.value, HealthSlider.value, speedHealthChange * Time.deltaTime);
            }
            else
            {
                break;
            }
            yield return null;
        }

        RoutineChangeHealth = null;
    }
}
