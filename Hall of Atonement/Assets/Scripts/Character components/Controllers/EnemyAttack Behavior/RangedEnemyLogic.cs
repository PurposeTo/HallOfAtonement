using UnityEngine;

[RequireComponent(typeof(IRanged))]
public class RangedEnemyLogic : MonoBehaviour, IEnemyAttackBehavior
{
    private bool getСloser = false;

    //private IRanged rangeWeapon;
    private float AttackRange;

    private void Start()
    {
        AttackRange = gameObject.GetComponent<EnemyPresenter>().MyEnemyStats.ViewingRadius;
    }


    Vector2 IEnemyAttackBehavior.GetMovingVectorOnFighting(GameObject focusTarget)
    {
        Vector2 newInputVector = Vector2.zero;

        if (focusTarget != null)
        {
            Vector2 direction = (focusTarget.transform.position - transform.position); //Расстояние до цели

            // Если расстояние до цели меньше четверти, то нужно отойти
            if (direction.magnitude < (AttackRange / 2f))
            {
                getСloser = false;

                // Уходить от цели только если это НЕ игрок
                if (!focusTarget.TryGetComponent(out PlayerController _)) { newInputVector = -direction.normalized; }

            }
            // Если расстояние до цели больше чем три четверти, то нужно подойти
            else if (direction.magnitude >= (AttackRange / 2f))
            {
                if (getСloser)
                {
                    newInputVector = direction.normalized;
                }

                if (direction.magnitude > ((AttackRange / 4f) * 3f))
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
        }


        return newInputVector;
    }


    float IEnemyAttackBehavior.GetAttackRange()
    {
        return AttackRange;
    }
}
