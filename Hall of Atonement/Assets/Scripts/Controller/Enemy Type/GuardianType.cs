using UnityEngine;

public class GuardianType : MonoBehaviour, ICharacterType
{
    private EnemyPresenter EnemyPresenter;


    private void Start()
    {
        EnemyPresenter = gameObject.GetComponent<EnemyPresenter>();
    }


    GameObject ICharacterType.SearchingTarget()
    {
        GameObject target;
        if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= EnemyPresenter.MyEnemyStats.ViewingRadius) 
        {
            target = GameManager.instance.player;
        }
        else
        {
            target = null;
        }

        return target;
    }

}
