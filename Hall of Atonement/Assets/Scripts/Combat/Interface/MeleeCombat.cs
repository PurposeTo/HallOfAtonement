using UnityEngine;

public class MeleeCombat : MonoBehaviour, IMelee
{
    public void Attack(CharacterCombat combat)
    {
        print(gameObject.name + " использует ближнюю атаку!");


        //Ударить
        //Меч попал во что то
        //Это что то имеет Статы?
        //if(hit.gameObject.GetComponent<CharacterStats>())
        //{
        //    combat.DoDamage(hit.gameObject.GetComponent<CharacterStats>());
        //}
    }
}
