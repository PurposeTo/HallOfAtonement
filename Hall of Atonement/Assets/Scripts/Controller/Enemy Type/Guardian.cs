using UnityEngine;

public class Guardian : MonoBehaviour, IEnemyType
{
    GameObject IEnemyType.SearchingTarget(float ViewingRadius)
    {
        GameObject target;
        if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= ViewingRadius) 
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
