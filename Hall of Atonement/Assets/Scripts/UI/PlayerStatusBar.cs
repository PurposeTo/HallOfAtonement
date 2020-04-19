using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusBar : MonoBehaviour
{
    public GameObject ImagePrefab;

    private Transform StatusBarContain => gameObject.transform;

    private Dictionary<StatusEffect, StatusEffectObject> statusEffectObjects = new Dictionary<StatusEffect, StatusEffectObject>();


    private class StatusEffectObject
    {
        public StatusEffectObject(GameObject _gameObject, StatusEffectImage statusEffectImage) 
        { 
            this._gameObject = _gameObject;
            this.statusEffectImage = statusEffectImage;
        }


        private readonly GameObject _gameObject;
        private readonly StatusEffectImage statusEffectImage;


        public GameObject GetGameObject() { return _gameObject; }
        public StatusEffectImage GetStatusEffectImage() { return statusEffectImage; }
    }


    public void AddStatusEffectToContaine(StatusEffect statusEffect)
    {
        GameObject statusEffectGameObject = ObjectPooler.Instance.SpawnFromPool(ImagePrefab, Vector3.zero, Quaternion.identity, StatusBarContain);
        StatusEffectImage statusEffectImage = statusEffectGameObject.GetComponent<StatusEffectImage>();

        StatusEffectObject newStatusEffectObject = new StatusEffectObject(statusEffectGameObject, statusEffectImage);
        statusEffectObjects.Add(statusEffect, newStatusEffectObject);

        if (statusEffect is ActiveEffect activeStatusEffect)
        {
            activeStatusEffect.onChangeDurationStatusEffect += ChangeFillingOutline;
        }

        statusEffectImage.Set(statusEffect.StatusEffectData.GetStatusEffectSprite(), statusEffect.StatusEffectData.StatusEffectType);
    }


    public void RemoveStatusEffectFromContaine(StatusEffect statusEffect)
    {
        Destroy(statusEffectObjects[statusEffect].GetGameObject());

        statusEffectObjects.Remove(statusEffect);
    }


    private void ChangeFillingOutline(ActiveEffect ActiveStatusEffect, float filling)
    {
        statusEffectObjects[ActiveStatusEffect].GetStatusEffectImage().ChangeFillingOutline(filling);
    }
}
