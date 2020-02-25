using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CharacterStats MyStats;
    public Slider HealthSlider;
    public Slider decreasingValueSlider;
    public Text HealthPointText;
    public Text HealthRegenText;

    private float maxSliderValue = 100f;

    private readonly float rateOfDecrease = 15f;
    private readonly float delayBeforeChange = 0.3f;

    private Coroutine RoutineChangeHealth = null;


    public void Start()
    {
        HealthSlider.maxValue = maxSliderValue;
        HealthSlider.value = maxSliderValue;
        decreasingValueSlider.maxValue = maxSliderValue;
        decreasingValueSlider.value = maxSliderValue;
        ShowHealthPoinOnText();
    }

    //Сделать по нормальному! (Скорее всего значение регенерации вообще не будет показываться, так что можно не менять)
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
            decreasingValueSlider.value = decreasingValueSlider.value;
        }

        ShowHealthPoinOnText();
    }

    private IEnumerator ChangeHealthFill()
    {
        yield return new WaitForSeconds(delayBeforeChange);

        while (true)
        {
            if (decreasingValueSlider.value > HealthSlider.value)
            {
                decreasingValueSlider.value = Mathf.MoveTowards(decreasingValueSlider.value, HealthSlider.value, rateOfDecrease * Time.deltaTime);
            }
            else if (decreasingValueSlider.value < HealthSlider.value)
            {
                decreasingValueSlider.value = decreasingValueSlider.value;
                break;
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
