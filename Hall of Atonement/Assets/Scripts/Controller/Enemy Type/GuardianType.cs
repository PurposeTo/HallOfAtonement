using UnityEngine;

public class GuardianType : MonoBehaviour, ICharacterType
{
    GameObject ICharacterType.SearchingTarget(float ViewingRadius)
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
