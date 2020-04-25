using UnityEngine;

public class GuardianType : EnemyType
{
    public override GameObject SearchingTarget()
    {
        GameObject target;
        if (Vector2.Distance(PlayerController.Instance.gameObject.transform.position, transform.position) <= EnemyPresenter.MyEnemyStats.ViewingRadius) 
        {
            target = PlayerController.Instance.gameObject;
        }
        else
        {
            target = null;
        }

        return target;
    }

}
