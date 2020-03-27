using UnityEngine;

public abstract class BoxStats : UnitStats
{
    private protected override float BaseMaxHealthPoint { get; } = 40f;
    private protected override float basePoisonResistanceValue { get; } = 1f;
    private protected override float baseBleedingResistanceValue { get; } = 1f;

    public override void Die(CharacterStats killerStats)
    {
        Debug.Log(transform.name + " Разрушен!");
        Destroy(gameObject);
    }
}
