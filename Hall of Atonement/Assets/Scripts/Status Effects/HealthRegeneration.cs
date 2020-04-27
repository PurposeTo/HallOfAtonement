using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ВНИМАНИЕ! Баг!
 * При хиле происходит изменение ХП, которое СРАЗУ же вызывает этот же метод, из за чего цикл становится бесконечным!
 * Баг все еще есть!
 */
public class HealthRegeneration : StatusEffect, IHealLogic
{
    private protected override ContainerStatusEffects StatusEffectType => throw new System.NotImplementedException(); // Позже изменить

    public override StatusEffectData StatusEffectData => StatusEffectDataContainer.Instance.GetStatusEffectData(StatusEffectType);


    private CharacterPresenter characterPresenter;
    private CharacterStats myStats;

    private float hpRegenPercent = 0.01f;


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
        yield return null; // Пропустить кадр, что бы избежать переполнение стека

        Stat maxHealthPoint = myStats.maxHealthPoint;

        while (myStats.CurrentHealthPoint < maxHealthPoint.GetValue())
        {
            float _healing = maxHealthPoint.GetValue() * hpRegenPercent * Time.deltaTime;
            myStats.Healing(_healing);

            yield return null;
        }

        healingRoutine = null;
    }
}
