using UnityEngine;

class Freeze : MonoBehaviour, IDamageLogic, IStatModifier
{
    float IStatModifier.ModifierValue { get => slowly; }

    private float slowly = -10f; //Временный тест


    private void OnEnable()
    {
        gameObject.GetComponent<CharacterStats>().attackSpeed.AddModifier(this); //Временный тест
    }

    private void OnDisable()
    {
        gameObject.GetComponent<CharacterStats>().attackSpeed.RemoveModifier(this); //Временный тест
    }


    public void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        throw new System.NotImplementedException();
    }

    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats killerStats)
    {
        throw new System.NotImplementedException();
    }

    public void SelfDestruction()
    {
        // Нанести весь урон от льда
        Destroy(this);
    }
}
