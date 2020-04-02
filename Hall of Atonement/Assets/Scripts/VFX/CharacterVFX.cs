using UnityEngine;
using UnityEngine.UI;

public class CharacterVFX : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab;

    public void DisplayDamageTaken(bool isEvaded, bool isBlocked, DamageType damageType, float damage)
    {
        GameObject _textPrefab = UIObjectPooler.SharedInstance.SpawnFromPool(textPrefab, gameObject.transform.position, Quaternion.identity);
        Text textScript = _textPrefab.GetComponent<Text>();
        textScript.text = transform.name + ": isEvaded = " + isEvaded + "; isBlocked = " + isBlocked + "; damage = " + damage + " " + damageType;

        if (isEvaded)
        {
            Debug.Log(transform.name + " dodge the damage!"); //Задоджили урон!
            textScript.text = "Dodge";
        }
        else if (isBlocked)
        {
            Debug.Log(transform.name + " blocked the " + damageType);
            textScript.text = "Blocked the " + damageType;
        }
        else
        {
            Debug.Log(transform.name + " takes " + damage + " " + damageType);
            textScript.text = "-" + damage;

        }

    }
}
