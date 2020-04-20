using UnityEngine;

public class GuardianType : EnemyType
{
    public override GameObject SearchingTarget()
    {
        GameObject target;
        if (Vector2.Distance(LevelController.Instance.player.transform.position, transform.position) <= EnemyPresenter.MyEnemyStats.ViewingRadius) 
        {
            target = LevelController.Instance.player;
        }
        else
        {
            target = null;
        }

        return target;
    }

}
