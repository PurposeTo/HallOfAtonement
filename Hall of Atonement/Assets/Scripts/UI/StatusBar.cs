using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public GameObject ImagePrefab;

    private Transform statusBarContain;


    private void Start()
    {
        statusBarContain = gameObject.transform;
    }


    public void AddStatusEffectToContaine(IStatusEffectLogic statusEffect)
    {
        //GameObject statusEffectObject = Instantiate(ImagePrefab, statusBarContain);
        //Image statusEffectImage = statusEffectObject.GetComponent<Image>();
        //statusEffectImage.sprite = sprite;

    }


    public void RemoveStatusEffectFromContaine(IStatusEffectLogic statusEffect)
    {

    }
}
