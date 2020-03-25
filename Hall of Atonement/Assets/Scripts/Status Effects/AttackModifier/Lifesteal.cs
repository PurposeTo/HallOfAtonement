using UnityEngine;

public class Lifesteal : MonoBehaviour, IAttackModifier
{
    private CharacterPresenter characterPresenter;
    private const float baseLifestealValue = 0.15f;
    private int baseEffectPower = 1; // Эффект работает даже при 0-ом мастерстве

    private int masteryPointsForBleeding = 5;


    private void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();
        characterPresenter.Combat.attackModifiers.Add(this);
    }


    private void OnDestroy()
    {
        characterPresenter.Combat.attackModifiers.Remove(this);
    }


    public void ApplyAttackModifier(UnitStats targetStats, DamageType damageType, float damage, int mastery, bool isCritical = false)
    {
        mastery += baseEffectPower;
        characterPresenter.MyStats.Healing(damage * (1 - targetStats.bleedingResistance.GetValue()) * baseLifestealValue * mastery); // Lifesteal снижается сопротивлением к кровотечению
        BleedingFromLifesteal(targetStats, mastery, damage);
        Debug.Log(gameObject.name + ": \"Your life is mine!\"");
    }


    private void BleedingFromLifesteal(UnitStats targetStats, int mastery, float damage)
    {
        if (mastery >= 15)
        {
            new StatusEffectFactory<Bleeding>(targetStats.gameObject, characterPresenter.MyStats, mastery/masteryPointsForBleeding);
        }
    }


    public object Clone()
    {
        return MemberwiseClone();
    }
}
