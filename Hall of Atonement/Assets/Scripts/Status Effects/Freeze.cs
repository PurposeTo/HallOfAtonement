using UnityEngine;

class Freeze : MonoBehaviour, IDamageLogic
{
    public void HangStatusEffect()
    {
        Debug.Log(gameObject.name + ": \"!\"");
    }

    public void StatusEffectDamage(UnitStats targetStats, CharacterStats killerStats, DamageType damageType)
    {
        throw new System.NotImplementedException();
    }
}
