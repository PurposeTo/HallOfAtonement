using UnityEngine;

public class GuardianType : EnemyType
{
    public override GameObject SearchingTarget()
    {
        GameObject target;
        if (Vector2.Distance(GameManager.Instance.Player.transform.position, transform.position) <= EnemyPresenter.MyEnemyStats.ViewingRadius) 
        {
            target = GameManager.Instance.Player;
        }
        else
        {
            target = null;
        }

        return target;
    }

}
