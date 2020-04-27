using UnityEngine;

public class StatusEffectFactory<T> where T : ActiveEffect
{
    public StatusEffectFactory(UnitStats targetStats, CharacterStats ownerStats, float amplificationAmount)
    {
        System.Type effectType = typeof(T);

        bool isBlocked = false;


        if (Equals(effectType, typeof(Burn)))
        {
            if (Mathf.Equals(targetStats.fireResistance.GetValue(), 1f))
            {
                isBlocked = true;
            }
        }
        else if (Equals(effectType, typeof(Freeze)))
        {
            if (Mathf.Equals(targetStats.iceResistance.GetValue(), 1f))
            {
                isBlocked = true;
            }
        }
        else if (Equals(effectType, typeof(Poisoning)))
        {
            if (Mathf.Equals(targetStats.poisonResistance.GetValue(), 1f))
            {
                isBlocked = true;
            }
        }
        else if (Equals(effectType, typeof(Bleeding)))
        {
            if (Mathf.Equals(targetStats.bleedingResistance.GetValue(), 1f))
            {
                isBlocked = true;
            }
        }
        else
        {
            isBlocked = false;
        }



        if (!isBlocked)
        {
            GameObject target = targetStats.gameObject;

            ActiveEffect effect = target.GetComponent<T>();

            if (effect == null)
            {
                effect = target.AddComponent<T>();
            }

            //Сделать зависимость силы эффекта от урона или от mastery
            effect.AmplifyEffect(ownerStats, amplificationAmount);
        }
    }
}