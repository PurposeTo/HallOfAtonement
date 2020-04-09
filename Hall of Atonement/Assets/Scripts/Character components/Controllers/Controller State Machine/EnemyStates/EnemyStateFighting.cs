using System.Collections;
using UnityEngine;

public class EnemyStateFighting : EnemyStateMachine
{
    private GameObject focusTarget; //Цель, на которой враг в данный момент сфокусирован

    private float timer = 3f;

    private Coroutine fightingRoutine;

    private Vector2 inputVector;


    private protected override void StopTheAction(EnemyAI enemyAI)
    {
        if (fightingRoutine != null)
        {
            StopCoroutine(fightingRoutine);
            fightingRoutine = null;
        }
        enemyAI.EnemyPresenter.Combat.targetToAttack = null;

        inputVector = Vector2.zero;
    }


    public override void Fighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        if (fightingRoutine == null)
        {
            this.focusTarget = focusTarget;

            fightingRoutine = StartCoroutine(FightingEnumerator(enemyAI));
        }
        else
        {
            //Если цель слишком далеко или ее вообше нет, то сменить цель на новую предложенную
            if (this.focusTarget == null || Vector2.Distance(this.focusTarget.transform.position, transform.position) <= enemyAI.EnemyPresenter.MyEnemyStats.ViewingRadius)
            {
                this.focusTarget = focusTarget;
            }
        }
    }


    public override void SeekingBattle(EnemyAI enemyAI)
    {
        // Если цель еще жива, то стоит преследовать ее
        if (focusTarget == null)
        {
            StopTheAction(enemyAI);
            enemyAI.EnemyStateMachine = enemyAI.EnemyStatePatrolling;
            enemyAI.EnemyStateMachine.SeekingBattle(enemyAI);
        }
        else
        {
            fightingRoutine = StartCoroutine(FightingEnumerator(enemyAI));
        }
    }


    private IEnumerator FightingEnumerator(EnemyAI enemyAI)
    {
        //Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;

        float timerCounter = timer;

        while (timerCounter > 0f && focusTarget != null)
        {
            enemyAI.EnemyPresenter.EnemyCombat.GetEnemyFightingLogic(focusTarget);

            inputVector = enemyAI.EnemyPresenter.EnemyCombat.GetMovingVectorOnFighting(focusTarget);

            if (enemyAI.CharacterPresenter.Combat.targetToAttack != null && Vector2.Distance(enemyAI.CharacterPresenter.Combat.targetToAttack.transform.position, transform.position) <= enemyAI.EnemyPresenter.MyEnemyStats.ViewingRadius)
            {
                enemyAI.CharacterPresenter.Combat.targetToAttack = null;
            }

            yield return null;
            timerCounter -= Time.deltaTime;
        }

        fightingRoutine = null;
        //Когда закончим, вызвать метод, говорящее о том, что мы закончили
        enemyAI.DecideWhatToDo();
    }

    public override Vector2 GetInputVector()
    {
        return inputVector;
    }
}
