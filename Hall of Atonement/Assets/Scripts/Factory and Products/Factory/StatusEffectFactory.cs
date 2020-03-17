public class StatusEffectFactory
{
    public void HangStatusEffect(DamageType damageType, UnitStats targetStats, CharacterStats ownerStats, float amplificationAmount)
    {
        IDamageLogic effect = null;

        if (damageType is FireDamage)
        {
            effect = targetStats.gameObject.GetComponent<Burn>();

            if (effect == null)
            {
                effect = targetStats.gameObject.AddComponent<Burn>();
            }
        }
        else if (damageType is IceDamage)
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
