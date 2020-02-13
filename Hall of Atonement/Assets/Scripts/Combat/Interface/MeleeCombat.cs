using UnityEngine;

public class MeleeCombat : MonoBehaviour, IAttacker
{
    private protected CharacterCombat combat;

    public void Attack()
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

    private protected virtual void Start()
    {
        combat = gameObject.GetComponent<CharacterCombat>();
    }
}
