public class PlayerStats : CharacterStats
{
    public override void GetExperience(int amount)
    {
        base.GetExperience(amount / 4);
    }


    public override void Die(CharacterStats killerStats)
    {
        base.Die(killerStats);
        GameManager.Instance.EnterTheHall(); //ТУТ! смерть!
    }
}