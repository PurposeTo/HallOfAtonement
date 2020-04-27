using UnityEngine;

public class OverflowingHPContainer
{
    private const int overflowingHPCoefficient = 6;

    private float overflowingHealthPoints;

    public CharacteristicModifier<float> AttackDamageForOverflowingHP { get; private set; } = new CharacteristicModifier<float>();
    public CharacteristicModifier<float> MaxHPForOverflowingHP { get; private set; } = new CharacteristicModifier<float>();


    public float GetOverflowingHealthPoints()
    {
        return overflowingHealthPoints;
    }


    public void AddOverflowingHealthPoints(float value)
    {
        if (value >= 0f)
        {
            overflowingHealthPoints += value;

            while (overflowingHealthPoints >= overflowingHPCoefficient)
            {
                overflowingHealthPoints -= overflowingHPCoefficient;
                AttackDamageForOverflowingHP.SetModifierValue(AttackDamageForOverflowingHP.GetModifierValue() + 1f);
                MaxHPForOverflowingHP.SetModifierValue(MaxHPForOverflowingHP.GetModifierValue() + 1f);
            }

        }
        else
        {
            Debug.LogWarning("Try to add negative overflowing HealthPoints");
        }
    }

}
