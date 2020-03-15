using UnityEngine;

public class MonsterType : MonoBehaviour, ICharacterType
{
    private EnemyPresenter EnemyPresenter;


    private void Start()
    {
        EnemyPresenter = gameObject.GetComponent<EnemyPresenter>();
    }


    GameObject ICharacterType.SearchingTarget()
    {
        GameObject target = null;


        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), EnemyPresenter.MyEnemyStats.ViewingRadius);

        float distance = float.MaxValue;

        for (int i = 0; i < colliders.Length; i++)
        {
            //if (colliders[i].gameObject != gameObject && !colliders[i].isTrigger) { }
            if (colliders[i].gameObject != gameObject &&
                (colliders[i].gameObject.TryGetComponent(out MonsterType _) || colliders[i].gameObject.TryGetComponent(out PlayerController _)))
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
