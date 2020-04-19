using TMPro;
using UnityEngine;

public class VFXManager : Singleton<VFXManager>
{
    [SerializeField] private GameObject PopupTextPrefab;


    public void DisplayPopupText(Vector3 position, string text, float fontSize = 4f)
    {
        Color defaultColor = Color.white;

        DisplayPopupText(position, text, defaultColor, fontSize);

    }


    public void DisplayPopupText(Vector3 position, string text, Color color, float fontSize = 4f)
    {
        TextMeshPro textScript = SpawnPopupText(position);

        textScript.fontSize = fontSize;
        textScript.color = color;
        textScript.text = text;
    }


    private TextMeshPro SpawnPopupText(Vector3 position)
    {
        GameObject PopupTextObject = ObjectPooler.Instance.SpawnFromPool(PopupTextPrefab, position, Quaternion.identity);
        return PopupTextObject.GetComponent<TextMeshPro>();
    }
}
