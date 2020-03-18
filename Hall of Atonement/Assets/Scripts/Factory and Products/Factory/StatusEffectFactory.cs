﻿using UnityEngine;

public class StatusEffectFactory<T> where T : HangingEffect
{
    public StatusEffectFactory(GameObject target, CharacterStats ownerStats, float amplificationAmount)
    {
        HangingEffect effect = target.GetComponent<T>();

        if (effect == null)
        {
            effect = target.AddComponent<T>();
        }

        //Сделать зависимость силы эффекта от урона или от mastery
        effect.AmplifyEffect(ownerStats, amplificationAmount);
    }
}