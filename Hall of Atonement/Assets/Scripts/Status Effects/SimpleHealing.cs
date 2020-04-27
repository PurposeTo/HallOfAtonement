using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ВНИМАНИЕ! Баг!
 * При хиле происходит изменение ХП, которое СРАЗУ же вызывает этот же метод, из за чего цикл становится бесконечным!
 * Баг все еще есть!
 */
public class SimpleHealing : StatusEffect, IHealLogic
{
    private protected override ContainerStatusEffects StatusEffectType => throw new System.NotImplementedException(); // позже изменить

    public override StatusEffectData StatusEffectData => StatusEffectDataContainer.Instance.GetStatusEffectData(StatusEffectType);


    private CharacterPresenter characterPresenter;
    private CharacterStats myStats;

    private float healingPerSec = 2f;


    private Coroutine healingRoutine;


    private void Start()
    {
        characterPresenter = gameObject.GetComponent<CharacterPresenter>();
        myStats = characterPresenter.MyStats;

        characterPresenter.AddStatusEffect(this);

        characterPresenter.MyStats.OnChangedCurrentHealth += CheckCurrentHealth;
    }


    private void OnDestroy()
    {
        characterPresenter.RemoveStatusEffect(this);

        myStats.OnChangedCurrentHealth -= CheckCurrentHealth;
    }


    private void CheckCurrentHealth()
    {
        if (healingRoutine == null)
        {
            healingRoutine = StartCoroutine(HealingEnumerator());
        }
    }


    private IEnumerator HealingEnumerator()
    {
        yield return null; // Не очень понимаю, зачем пропускать кадр?

        while (myStats.CurrentHealthPoint < myStats.maxHealthPoint.GetValue())
        {
            float _healing = healingPerSec * Time.deltaTime;
            myStats.Healing(_healing);

            yield return null;
        }

        healingRoutine = null;
    }
}
