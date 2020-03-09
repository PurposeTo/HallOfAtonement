using System.Collections;
using UnityEngine;

public class EnemyStateHunting : EnemyAIStateMachine
{
    private Coroutine huntingRoutine;

    private IEnemyAttackType enemyAttackType;

    private GameObject huntingTarget;


    private void Start()
    {
        enemyAttackType = gameObject.GetComponent<IEnemyAttackType>();
    }


    private protected override void StopTheAction(EnemyAI enemyAI)
    {
        if (huntingRoutine != null)
        {
            StopCoroutine(huntingRoutine);
            huntingRoutine = null;
            huntingTarget = null;
        }
    }


    public override void Fighting(EnemyAI enemyAI)
    {
        StopTheAction(enemyAI);

        enemyAI.EnemyAIStateMachine = enemyAI.EnemyStateFighting;
        enemyAI.EnemyAIStateMachine.Fighting(enemyAI);
    }

    public override void SeekingBattle(EnemyAI enemyAI)
    {
        if (huntingRoutine == null)
        {
            huntingRoutine = StartCoroutine(HuntingEnumerator(enemyAI));
        }
    }

    private IEnumerator HuntingEnumerator(EnemyAI enemyAI)
    {
        //Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;

        
        while (true)
        {
            //Преследовать цель, пока она жива. Если не жива, то Узнать что нам делать. 
            //Каждый раз проверять, есть ли вокруг нас враги

            if (huntingTarget != null) 
            {
                enemyAttackType.GetEnemyFightingLogic(enemyAI, huntingTarget);
            }
            yield return null;

            enemyAI.DecideWhatToDo();
        }    
    }


    public override void BeginTheHunt(EnemyAI enemyAI, GameObject huntingTarget)
    {
        if (huntingRoutine == null)
        {
            this.huntingTarget = huntingTarget;
            huntingRoutine = StartCoroutine(HuntingEnumerator(enemyAI));
        }
    }
}
