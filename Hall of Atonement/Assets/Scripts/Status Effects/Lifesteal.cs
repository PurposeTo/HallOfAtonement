using UnityEngine;

public class Lifesteal : MonoBehaviour, IAttackModifier
{
    //private CharacterStats ownerStats;
    //private CharacterCombat ownerCombat;
    private CharacterPresenter characterPresenter;
    private float lifestealValue = 0.05f;


    public void ApplyAttackModifier(float damage)
    {
        characterPresenter.MyStats.Healing(damage * lifestealValue);
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

}
