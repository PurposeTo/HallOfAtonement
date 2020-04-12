using UnityEngine;
using UnityEngine.UI;

public class StatusEffectImage : MonoBehaviour
{
    [SerializeField] private Image outlineImage;
    [SerializeField] private Image statusEffectImage;
    private ContainerEffectTypes EffectType;

    public void Set(Sprite sprite, ContainerEffectTypes EffectType)
    {
        statusEffectImage.sprite = sprite;
        this.EffectType = EffectType;
        ChangeOutlineColour();
        ChangeFillingOutline(1f);
    }


    public void ChangeFillingOutline(float filling)
    {
        outlineImage.fillAmount = filling;
    }


    private void ChangeOutlineColour()
    {
        float r;
        float g;
        float b;

        switch (EffectType)
        {
            case ContainerEffectTypes.Positive:
                r = 0.23f;
                g = 0.7f;
                b = 0.18f;
                outlineImage.color = new Color(r, g, b);
                break;
            case ContainerEffectTypes.Negative:
                r = 0.82f;
                g = 0.16f;
                b = 0.05f;
                outlineImage.color = new Color(r, g, b);
                break;
            default:
                Debug.LogError(gameObject.name + " : error in ContainerEffect Type!");
                break;

                // Нельзя выбрать данные типы урона в качестве атаки!
                //case ContainerDamageTypes.BleedingDamage:
                //    Debug.Log(gameObject.name + " choise the BleedingDamage!");
                //    DamageType = new BleedingDamage();
                //    break;
                //case ContainerDamageTypes.PoisonDamage:
                //    Debug.Log(gameObject.name + " choise the PoisonDamage!");
                //    DamageType = new PoisonDamage();
                //    break;
        }
    }

}
