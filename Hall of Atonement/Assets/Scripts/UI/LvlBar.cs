using UnityEngine;
using UnityEngine.UI;

public class LvlBar : MonoBehaviour
{
    public CharacterStats MyStats;
    public Text LvlText;

    private void Start()
    {
        MyStats.level.OnLevelUp += ShowLvl;
        ShowLvl();
    }


    private void OnDestroy()
    {
        MyStats.level.OnLevelUp -= ShowLvl;
    }


    public void ShowLvl()
    {
        LvlText.text = MyStats.level.GetLvl() + "";
    }
}
