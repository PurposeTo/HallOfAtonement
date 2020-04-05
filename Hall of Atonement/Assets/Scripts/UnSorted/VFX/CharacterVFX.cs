using TMPro;
using UnityEngine;

public class CharacterVFX : MonoBehaviour
{
    [SerializeField] private GameObject PopupTextPrefab;


    public void DisplayPopupText(string text, float fontSize = 4f)
    {
        Color defaultColor = Color.white;

        DisplayPopupText(text, defaultColor, fontSize);

    }


    public void DisplayPopupText(string text, Color color, float fontSize = 4f)
    {
        TextMeshPro textScript = SpawnPopupText();

        textScript.fontSize = fontSize;
        textScript.color = color;
        textScript.text = text;
    }


    private TextMeshPro SpawnPopupText()
    {
        GameObject PopupTextObject = ObjectPooler.SharedInstance.SpawnFromPool(PopupTextPrefab, gameObject.transform.position, Quaternion.identity);
        return PopupTextObject.GetComponent<TextMeshPro>();
    }
}
