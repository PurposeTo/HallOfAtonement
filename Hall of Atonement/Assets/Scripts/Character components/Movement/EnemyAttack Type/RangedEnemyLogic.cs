using UnityEngine;

//[RequireComponent(typeof(RangedLaserCombat))]
public class RangedEnemyLogic : EnemyCombat
{
    private bool getСloser = false;


    private protected override void Start()
    {
        base.Start();

        AttackRange = EnemyPresenter.MyEnemyStats.ViewingRadius;

    }


    private protected override Vector2 GetMovingVectorOnFighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        Vector2 newInputVector = Vector2.zero;

        Vector2 direction = (focusTarget.transform.position - transform.position); //Расстояние до цели

        // Если расстояние до цели меньше четверти, то нужно отойти
        if (direction.magnitude < (enemyAI.EnemyPresenter.MyEnemyStats.ViewingRadius / 2f))
        {
            getСloser = false;

            // Уходить от цели только если это НЕ игрок
            if (!focusTarget.TryGetComponent(out PlayerMovement _)) { newInputVector = -direction.normalized; }
            
        }
        // Если расстояние до цели больше чем три четверти, то нужно подойти
        else if (direction.magnitude >= (enemyAI.EnemyPresenter.MyEnemyStats.ViewingRadius / 2f))
        {
            if (getСloser)
            {
                newInputVector = direction.normalized;
            }

            if (direction.magnitude > ((enemyAI.EnemyPresenter.MyEnemyStats.ViewingRadius / 4f) * 3f))
            {
                // Если расстояние до цели больше 3/4, то начать подходить
                getСloser = true;
                newInputVector = direction.normalized;
            }


        }
        else
        {
            getСloser = false;
            newInputVector = Vector2.zero;
        }


        return newInputVector;
    }
}
