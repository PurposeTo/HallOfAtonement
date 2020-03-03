using UnityEngine;

//[RequireComponent(typeof(IEnemyMode))]
public class Guardian : MonoBehaviour, IEnemyType
{
    public GameObject SearchingTarget(float ViewingRadius)
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
