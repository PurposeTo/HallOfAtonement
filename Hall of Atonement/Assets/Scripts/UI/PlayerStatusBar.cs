using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBar : MonoBehaviour
{
    public GameObject ImagePrefab;

    private Transform StatusBarContain => gameObject.transform;

    private Dictionary<IStatusEffectLogic, GameObject> statusEffectObjects = new Dictionary<IStatusEffectLogic, GameObject>();


    public void AddStatusEffectToContaine(IStatusEffectLogic statusEffect)
    {
        //GameObject statusEffectObject = Instantiate(ImagePrefab, statusBarContain);
        GameObject statusEffectObject = UIObjectPooler.SharedInstance.SpawnFromPool(ImagePrefab, Vector3.zero, Quaternion.identity, StatusBarContain);
        statusEffectObjects.Add(statusEffect, statusEffectObject);
        Image statusEffectImage = statusEffectObject.GetComponent<Image>();
        statusEffectImage.sprite = statusEffect.StatusEffectSprite;
    }


    public void RemoveStatusEffectFromContaine(IStatusEffectLogic statusEffect)
    {
        Destroy(statusEffectObjects[statusEffect]);
        statusEffectObjects.Remove(statusEffect);
    }
}
