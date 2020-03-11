using UnityEngine;

public class Lifesteal : MonoBehaviour, IAttackModifier
{
    private CharacterStats ownerStats;
    private CharacterCombat ownerCombat;
    private float lifestealValue = 0.05f;


    public void ApplyAttackModifier(float damage)
    {
        ownerStats.Healing(damage * lifestealValue);
        Debug.Log(ownerStats.gameObject.name + ": \"Your life is mine!\"");
    }


    void Start()
    {
        ownerStats = gameObject.GetComponent<CharacterStats>();
        ownerCombat = gameObject.GetComponent<CharacterCombat>();
        ownerCombat.attackModifiers.Add(this);
    }
}
