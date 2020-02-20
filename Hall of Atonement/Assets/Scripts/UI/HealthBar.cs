using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CharacterStats MyStats;
    public Slider HealthSlider;
    public Slider HealthChangeSlider;
    public Text HealthPointText;
    public Text HealthRegenText;

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
        ShowHealthPoinOnText();
    }

    //Сделать по нормальному!
    private void Update()
    {
        HealthRegenText.text = "+" + System.Math.Round(MyStats.healthPointRegen.GetValue(), 1) + " ";
    }


    private void ShowHealthPoinOnText()
    {
        HealthPointText.text = (int)System.Math.Truncate(MyStats.CurrentHealthPoint) + "/" 
            + (int)System.Math.Truncate(MyStats.maxHealthPoint.GetValue());
    }


    public void DecreaseHealthBar()
    {
        HealthSlider.value = MyStats.CurrentHealthPoint / MyStats.maxHealthPoint.GetValue() * maxSliderValue;

        if (RoutineChangeHealth == null)
        {
            RoutineChangeHealth = StartCoroutine(ChangeHealthFill());
        }
    }


    public void IncreaseHealthBar()
    {
        HealthSlider.value = MyStats.CurrentHealthPoint / MyStats.maxHealthPoint.GetValue() * maxSliderValue;

        if (RoutineChangeHealth == null)
        {
            HealthChangeSlider.value = HealthChangeSlider.value;
        }

        ShowHealthPoinOnText();
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
            ShowHealthPoinOnText();
            yield return null;
        }

        RoutineChangeHealth = null;
    }
}
