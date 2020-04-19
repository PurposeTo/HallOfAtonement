using UnityEngine;

public class GuardianType : EnemyType
{
    public override GameObject SearchingTarget()
    {
        GameObject target;
        if (Vector2.Distance(GameManager.Instance.player.transform.position, transform.position) <= EnemyPresenter.MyEnemyStats.ViewingRadius) 
        {
            target = GameManager.Instance.player;
        }
        else
        {
            target = null;
        }

        return target;
    }

}
