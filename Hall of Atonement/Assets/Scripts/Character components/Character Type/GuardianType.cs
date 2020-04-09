using UnityEngine;

public class GuardianType : EnemyType
{
    public override GameObject SearchingTarget()
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
