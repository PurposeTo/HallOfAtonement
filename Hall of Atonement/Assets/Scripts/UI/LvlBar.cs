using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LvlBar : MonoBehaviour
{
    public CharacterStats MyStats;
    public Text LvlText;

    private Coroutine coroutineLevelUp;


    private void Start()
    {
        MyStats.level.OnLevelUp += UpdateItem;
        ShowLvl();
    }


    private void OnDestroy()
    {
        MyStats.level.OnLevelUp -= UpdateItem;
    }


    private void UpdateItem()
    {
        if (coroutineLevelUp == null)
        {
            coroutineLevelUp = StartCoroutine(EnumeratorReportLevelUp());
        }
    }


    public void ShowLvl()
    {
        LvlText.text = MyStats.level.GetLvl() + "";
    }


    // Обновить уровень нужно только через кадр, так как за раз может быть множество вызовов
    private IEnumerator EnumeratorReportLevelUp()
    {
        yield return null;
        ShowLvl();

        coroutineLevelUp = null;
    }
}
