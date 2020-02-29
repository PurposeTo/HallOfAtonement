public class StatusEffectFactory
{
    public void HangStatusEffect(DamageType damageType, UnitStats targetStats, CharacterStats ownerStats)
    {
        if (damageType is FireDamage)
        {
            Burn burn = targetStats.gameObject.GetComponent<Burn>();

            if (burn == null)
            {
                burn = targetStats.gameObject.AddComponent<Burn>();
            }

            //Сделать зависимость силы эффекта от урона или от mastery
            burn.AmplifyEffect(ownerStats, 1f);
        }
        else if (damageType is IceDamage)
        {
            Freeze freeze = targetStats.gameObject.GetComponent<Freeze>();

            if (freeze == null)
            {
                freeze = targetStats.gameObject.AddComponent<Freeze>();
            }

            //Сделать зависимость силы эффекта от урона или от mastery
            freeze.AmplifyEffect(ownerStats, 1f);
        }
    }
}
