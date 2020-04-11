using UnityEngine;

public abstract class BoxStats : UnitStats
{
    private protected override float BaseMaxHealthPoint { get; } = 40f;
    private protected override float basePoisonResistanceValue { get; } = 1f;
    private protected override float baseBleedingResistanceValue { get; } = 1f;



    public override float TakeDamage(CharacterStats killerStats, DamageType damageType, float damage, ref bool isEvaded, ref bool isBlocked, bool canEvade = true, bool isCritical = false, bool displayPopup = false)
    {
        // Box-ы не отображают PopUp полученный урон
        damage = base.TakeDamage(killerStats, damageType, damage, ref isEvaded, ref isBlocked, canEvade, isCritical, false);

        return damage;
    }
    public override void Die(CharacterStats killerStats)
    {
        Debug.Log(transform.name + " Разрушен!");
        Destroy(gameObject);
    }
}
