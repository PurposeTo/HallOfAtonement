using UnityEngine;
using UnityEngine.UI;

public class LvlBar : MonoBehaviour
{
    public CharacterStats MyStats;
    public Text LvlText;

    void Start()
    {
        ShowLvl();
    }

    public void ShowLvl()
    {
        LvlText.text = MyStats.level.GetLvl() + "";
    }
}
