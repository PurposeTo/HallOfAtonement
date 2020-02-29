public class BurnEffect : IStatusEffectProduct
{
    public void HangStatusEffect(UnitStats targetStats, CharacterStats ownerStats)
    {
        //Если у цели нет 100% сопротивления к огню
        if (targetStats.fireResistance.GetValue() != 1f)
        {
            Burn burn = targetStats.gameObject.GetComponent<Burn>();

            if (burn == null)
            {
                burn = targetStats.gameObject.AddComponent<Burn>();
            }

            //Сделать зависимость силы эффекта от урона или от mastery
            burn.AmplifyEffect(ownerStats, 1f);
        }
    }
}
