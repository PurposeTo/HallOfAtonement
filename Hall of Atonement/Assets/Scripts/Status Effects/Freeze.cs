using UnityEngine;

class Freeze : MonoBehaviour, IDamageLogic
{
    public void AmplifyEffect(CharacterStats ownerStats, float amplificationAmount)
    {
        throw new System.NotImplementedException();
    }

    public void DoStatusEffectDamage(UnitStats targetStats, CharacterStats killerStats, DamageType damageType)
    {
        throw new System.NotImplementedException();
    }

    public void SelfDestruction()
    {
        // Нанести весь урон от льда
        Destroy(this);
    }
}
