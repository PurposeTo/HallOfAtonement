using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    public override void GetExperience(int amount)
    {
        base.GetExperience(amount / 7);
    }


    public override void Die(CharacterStats killerStats)
    {
        base.Die(killerStats);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //ТУТ! смерть!
    }
}