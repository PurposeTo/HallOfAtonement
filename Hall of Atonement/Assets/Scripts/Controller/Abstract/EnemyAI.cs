using UnityEngine;


[RequireComponent(typeof(EnemyStats))]
public abstract class EnemyAI : CharacterController
{
    private protected GameObject targetToMove = null;

    [SerializeField] private protected virtual float LookRadius { get; set; } = 10f; //Расстояние, на котором враг заметит игрока
    private float minStopRadius; //Расстояние, на котором враг остановится
    private float maxStopRadius; //Расстояние, на котором враг опять начнет преследовать игрока


    private protected override void Start()
    {
        base.Start();

        maxStopRadius = gameObject.GetComponent<EnemyCombat>().AttackRange;
        LookRadius = Mathf.Clamp(LookRadius, maxStopRadius + 2f, float.MaxValue);
        minStopRadius = maxStopRadius / 2f;
    }


    private protected override void FixedUpdate()
    {
        targetToMove = SearchingTarget();

        base.FixedUpdate();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }


    private protected abstract GameObject SearchingTarget();


    private protected Vector2 MoveToTarget(GameObject targetMove)
    {
        Vector2 newInputVector;

        if (targetMove != null) //Если есть цель, к которой следует идти
        {
            Vector2 direction = (targetMove.transform.position - transform.position); //Расстояние до цели

            if (rb2D.velocity.magnitude > 1f)
            {
                //Если мы движемся, то двигаться пока расстояние до цели > minStopRadius
                if (direction.magnitude > minStopRadius)
                {
                    newInputVector = direction.normalized;
                }
                else
                {
                    newInputVector = Vector2.zero;
                }
            }
            else
            {
                //Если мы стоим, то стоять пока расстояние до цели не станет >= maxStopRadius
                if (direction.magnitude < maxStopRadius)
                {
                    newInputVector = Vector2.zero;
                }
                else
                {
                    newInputVector = direction.normalized;
                }
            }
        }
        else
        {
            newInputVector = Vector2.zero;
        }



        return newInputVector;
    }
}
