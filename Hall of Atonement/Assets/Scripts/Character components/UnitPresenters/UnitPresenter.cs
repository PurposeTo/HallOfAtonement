using System.Collections.Generic;
using UnityEngine;

public class UnitPresenter : MonoBehaviour
{
    public UnitStats UnitStats { get; private protected set; }

    public List<StatusEffect> StatusEffects { get; private protected set; } = new List<StatusEffect>(); // List<IStatusEffectLogic> находится в UnitStats, так как необходимо будет отображать "реакцию" на данный эффект, к примеру, Visual effect от Burn


    private protected virtual void Awake()
    {
        UnitStats = GetComponent<UnitStats>();
    }


    public virtual void AddStatusEffect(StatusEffect statusEffect)
    {
        StatusEffects.Add(statusEffect);
    }


    public virtual void RemoveStatusEffect(StatusEffect statusEffect)
    {
        StatusEffects.Remove(statusEffect);
    }
}
