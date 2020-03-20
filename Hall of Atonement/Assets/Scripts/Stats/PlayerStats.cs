using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController))]
public class PlayerStats : CharacterStats
{
    public override void GetExperience(int amount, out int numberOfNewLvls)
    {
        base.GetExperience(amount / 7, out numberOfNewLvls);

        print("~" + numberOfNewLvls);

        //Если уровень повысился
        for (int i = 0; i < numberOfNewLvls; i++)
        {
            int IndexRandomAttribute = Random.Range(0, allAttributes.Length);

            int newRandomAttributeValue = allAttributes[IndexRandomAttribute].GetBaseValue() + 1;
            allAttributes[IndexRandomAttribute].ChangeBaseValue(newRandomAttributeValue);

            Debug.Log(gameObject.name + " повысил уровень! Получите +1 к случайному атрибуту!");
        }
    }


    public override void Die(CharacterStats killerStats)
    {
        //base.Die(killerStats);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //ТУТ! смерть!
    }
}