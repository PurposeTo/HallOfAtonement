using UnityEngine;

public class Lifesteal : MonoBehaviour, IAttackModifier
{
    //private CharacterStats ownerStats;
    //private CharacterCombat ownerCombat;
    private CharacterPresenter characterPresenter;
    private const float baseLifestealValue = 0.15f;


    public void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery)
    {
        // К мастерсву необходимо прибавить еденицу, что бы эффект работал при нулевом мастерстве
        characterPresenter.MyStats.Healing(damage * baseLifestealValue * ++mastery);
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
