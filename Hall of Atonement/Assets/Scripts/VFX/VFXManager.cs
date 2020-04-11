using TMPro;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject PopupTextPrefab;

    public static VFXManager instance;

    private void Awake() //делаем объект синглтоном
    {
        if (instance == null)
        {
            instance = this;
        }
        //else if (instance != this)
        //{
        //    Destroy(gameObject);
        //}
        //DontDestroyOnLoad(gameObject);
    }

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
        GameObject PopupTextObject = ObjectPooler.SharedInstance.SpawnFromPool(PopupTextPrefab, position, Quaternion.identity);
        return PopupTextObject.GetComponent<TextMeshPro>();
    }
}
