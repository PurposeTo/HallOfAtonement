using UnityEngine;

//[RequireComponent(typeof(IEnemyMode))]
public class Monster : MonoBehaviour, IEnemyType
{
    public GameObject SearchingTarget(float ViewingRadius)
    {
        GameObject target = null;

        //Если игрок рядом, то отдать ему приоритет. Если далеко, то сражаться с монстрами
        if (Vector2.Distance(GameManager.instance.player.transform.position, transform.position) <= ViewingRadius)
        {
            target = GameManager.instance.player;
        }
        else
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), ViewingRadius);

            float distance;

            if (target == null)
            {
                distance = float.MaxValue; //Расстояние до цели
            }
            else
            {
                distance = Vector2.Distance(target.gameObject.transform.position, transform.position);
            }

            int enemyCount = 0;

            for (int i = 0; i < colliders.Length; i++)
            {
                //if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger) { }
                if (colliders[i].gameObject != gameObject && colliders[i].gameObject.TryGetComponent(out Monster _)) //Если это персонаж или монстер
                {
                    enemyCount++;

                    if (target != null)
                    {
                        float newDistance = Vector2.Distance(colliders[i].gameObject.transform.position, transform.position);
                        if ((newDistance + 2f) < distance)
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

            if (enemyCount == 0)
            {
                target = null;
            }
        }


        return target;
    }
}
