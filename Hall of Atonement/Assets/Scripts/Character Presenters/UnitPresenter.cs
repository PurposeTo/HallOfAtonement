using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitStats))]
public class UnitPresenter : MonoBehaviour
{
    public UnitStats UnitStats { get; private protected set; }

    public List<IStatusEffectLogic> StatusEffects { get; private protected set; } = new List<IStatusEffectLogic>(); // List<IStatusEffectLogic> находится в UnitStats, так как необходимо будет отображать "реакцию" на данный эффект, к примеру, Visual effect от Burn


    private protected virtual void Awake()
    {
        UnitStats = GetComponent<UnitStats>();
    }


    public virtual void AddStatusEffect(IStatusEffectLogic statusEffect)
    {
        StatusEffects.Add(statusEffect);
    }


    public virtual void RemoveStatusEffect(IStatusEffectLogic statusEffect)
    {
        StatusEffects.Remove(statusEffect);
    }
}
