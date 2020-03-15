using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyFireDefence : MonoBehaviour, IDefenseModifier
{
    private CharacterPresenter characterPresenter;

    void IDefenseModifier.ApplyDefenseModifier(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked)
    {
        if (killerStats == characterPresenter.MyStats) 
        {
            isEvaded = true;
        }
    }

    private void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();

        characterPresenter.MyStats.defenseModifiers.Add(this);
    }


    private void OnDestroy()
    {
        characterPresenter.MyStats.defenseModifiers.Remove(this);
    }
}
