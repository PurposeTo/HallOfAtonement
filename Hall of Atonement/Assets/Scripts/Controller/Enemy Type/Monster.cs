using UnityEngine;

public class Monster : MonoBehaviour, IEnemyType
{
    GameObject IEnemyType.SearchingTarget(float ViewingRadius)
    {
        GameObject target = null;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), ViewingRadius);

        float distance = float.MaxValue;

        for (int i = 0; i < colliders.Length; i++)
        {
            //if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger) { }
            if (colliders[i].gameObject != gameObject &&
                (colliders[i].gameObject.TryGetComponent(out Monster _) || colliders[i].gameObject.TryGetComponent(out PlayerController _)))
            //Если это игрок или монстр
            {

                if (target != null)
                {
                    float newDistance = Vector2.Distance(colliders[i].gameObject.transform.position, transform.position);
                    if (newDistance < distance)
                    {
                        //Если это не та же самая цель
                        if (colliders[i].gameObject != target)
                        {
                            target = colliders[i].gameObject;
                        }
                        distance = newDistance;
                    }
                }
                else
                {
                    target = colliders[i].gameObject;
                    distance = Vector2.Distance(target.transform.position, transform.position);
                }
            }
        }
        return target;
    }
}
