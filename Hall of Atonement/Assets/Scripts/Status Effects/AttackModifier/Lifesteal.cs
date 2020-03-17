using System;
using UnityEngine;

public class Lifesteal : MonoBehaviour, IAttackModifier
{
    //private CharacterStats ownerStats;
    //private CharacterCombat ownerCombat;
    private CharacterPresenter characterPresenter;
    private float baseLifestealValue = 0.05f;


    public void ApplyAttackModifier(float damage, int effectPower)
    {
        characterPresenter.MyStats.Healing(damage * baseLifestealValue * effectPower);
        Debug.Log(gameObject.name + ": \"Your life is mine!\"");
    }


    private void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();
        //ownerStats = gameObject.GetComponent<CharacterStats>();
        //ownerCombat = gameObject.GetComponent<CharacterCombat>();
        characterPresenter.Combat.attackModifiers.Add(this);
    }


    private void OnDestroy()
    {
        characterPresenter.Combat.attackModifiers.Remove(this);
    }


    public object Clone()
    {
        return MemberwiseClone();
    }
}
