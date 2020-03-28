using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    public GameObject ImagePrefab;

    private Transform statusBarContain;

    public List<IStatusEffectLogic> StatusEffects { get; private protected set; } = new List<IStatusEffectLogic>();


    private void Start()
    {
        statusBarContain = gameObject.transform;
    }


    public void AddStatusEffect(IStatusEffectLogic statusEffect, Sprite sprite = null)
    {
        StatusEffects.Add(statusEffect);
        GameObject statusEffectObject = Instantiate(ImagePrefab, statusBarContain);
        Image statusEffectImage = statusEffectObject.GetComponent<Image>();
        statusEffectImage.sprite = sprite;

    }


    public void RemoveStatusEffect(IStatusEffectLogic statusEffect)
    {
        StatusEffects.Remove(statusEffect);
    }
}
