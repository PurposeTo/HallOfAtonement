using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public CharacterStats MyStats;
    public Slider HealthSlider;
    public Slider decreasingHealthSlider;
    public Text HealthPointText;

    private float maxSliderValue = 1f;

    private readonly float rateOfDecrease = 0.15f;
    private readonly float delayBeforeChange = 0.3f;

    private Coroutine RoutineChangeHealth = null;


    private void OnEnable()
    {
        MyStats.OnChangedCurrentHealth += ChangeHealthBar;
    }


    private void Start()
    {
        Initialization();
    }


    private void OnDisable()
    {
        MyStats.OnChangedCurrentHealth -= ChangeHealthBar;
    }


    private void Initialization()
    {
        HealthSlider.maxValue = maxSliderValue;
        HealthSlider.value = maxSliderValue;
        decreasingHealthSlider.maxValue = maxSliderValue;
        decreasingHealthSlider.value = maxSliderValue;
        ShowHealthPoinOnText();
    }


    private void ShowHealthPoinOnText()
    {
        HealthPointText.text = Mathf.Round(MyStats.CurrentHealthPoint) + "/"
            + Mathf.Round(MyStats.maxHealthPoint.GetValue());
    }


    public void ChangeHealthBar()
    {
        HealthSlider.value = MyStats.CurrentHealthPoint / MyStats.maxHealthPoint.GetValue() * maxSliderValue;

        ShowHealthPoinOnText();

        if (decreasingHealthSlider.value > HealthSlider.value)
        {
            if (RoutineChangeHealth == null)
            {
                RoutineChangeHealth = StartCoroutine(ChangeHealthFill());
            }
        }
        else
        {
            decreasingHealthSlider.value = HealthSlider.value;

            if (RoutineChangeHealth != null)
            {
                StopCoroutine(ChangeHealthFill());
                RoutineChangeHealth = null;
            }
        }
    }


    private IEnumerator ChangeHealthFill()
    {

        yield return new WaitForSeconds(delayBeforeChange);


        while (true)
        {
            if (decreasingHealthSlider.value > HealthSlider.value)
            {
                decreasingHealthSlider.value = Mathf.MoveTowards(decreasingHealthSlider.value, HealthSlider.value, rateOfDecrease * Time.deltaTime);
            }
            else
            {
                decreasingHealthSlider.value = HealthSlider.value;
                break;
            }

            yield return null;
        }

        RoutineChangeHealth = null;
    }
}
