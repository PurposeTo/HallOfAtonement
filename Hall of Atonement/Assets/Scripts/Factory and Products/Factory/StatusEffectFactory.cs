﻿public class StatusEffectFactory
{
    public void HangStatusEffect(ItemHarding itemHarding)
    {
        IDamageLogic effect = null;

        if (itemHarding is Burn)
        {
            effect = targetStats.gameObject.GetComponent<Burn>();

            if (effect == null)
            {
                effect = targetStats.gameObject.AddComponent<Burn>();
            }
        }
        else if(itemHarding is Freeze)
        {
            effect = targetStats.gameObject.GetComponent<Freeze>();

            if (effect == null)
            {
                effect = targetStats.gameObject.AddComponent<Freeze>();
            }
        }

        //Сделать зависимость силы эффекта от урона или от mastery
        effect.AmplifyEffect(ownerStats, amplificationAmount);
    }
}