using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public GameObject ImagePrefab;

    private Transform statusBarContain;

    private Dictionary<IStatusEffectLogic, GameObject> statusEffectObjects = new Dictionary<IStatusEffectLogic, GameObject>();


    private void Awake()
    {
        statusBarContain = gameObject.transform;
    }


    public void AddStatusEffectToContaine(IStatusEffectLogic statusEffect)
    {
        GameObject statusEffectObject = Instantiate(ImagePrefab, statusBarContain);
        statusEffectObjects.Add(statusEffect, statusEffectObject);
        //Image statusEffectImage = statusEffectObject.GetComponent<Image>();
        //statusEffectImage.sprite = sprite;

    }


    public void RemoveStatusEffectFromContaine(IStatusEffectLogic statusEffect)
    {
        Destroy(statusEffectObjects[statusEffect]);
        statusEffectObjects.Remove(statusEffect);
    }
}
