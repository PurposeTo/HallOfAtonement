using System.Collections;
using UnityEngine;

public class EnemyStateFighting : EnemyAIStateMachine
{
    private GameObject focusTarget; //Цель, на которой враг в данный момент сфокусирован

    private readonly float timer = 3f;

    private Coroutine fightingRoutine;


    private protected override void StopTheAction(EnemyAI enemyAI)
    {
        if (fightingRoutine != null)
        {
            StopCoroutine(fightingRoutine);
            fightingRoutine = null;
        }
        enemyAI.EnemyPresenter.Combat.SetTargetToAttack(null);
    }


    public override void Fighting(EnemyAI enemyAI, GameObject focusTarget)
    {
        if (focusTarget != null)
        {

            GameObject oldTargetToAttack = enemyAI.CharacterPresenter.Combat.GetTargetToAttack();

            if (focusTarget != oldTargetToAttack && oldTargetToAttack != null)
            {
                // Обнулить targetToAttack, так как у нас новая цель, на которой мы сфокусировались
                enemyAI.CharacterPresenter.Combat.SetTargetToAttack(null);
            }

            if (fightingRoutine == null)
            {
                this.focusTarget = focusTarget;

                fightingRoutine = StartCoroutine(FightingEnumerator(enemyAI));
            }
            else
            {
                if (this.focusTarget != null)
                {
                    float distanceToOldFocusTarget = Vector2.Distance(this.focusTarget.transform.position, transform.position);
                    float distanceToNewFocusTarget = Vector2.Distance(focusTarget.transform.position, transform.position);

                    if ((distanceToOldFocusTarget > enemyAI.EnemyPresenter.MyEnemyStats.ViewingRadius) || distanceToNewFocusTarget < distanceToOldFocusTarget)
                    {
                        this.focusTarget = focusTarget;
                    }
                }
                else
                {
                    this.focusTarget = focusTarget;
                }
            }
        }
    }


    public override void SeekingBattle(EnemyAI enemyAI)
    {
        // Если цель еще жива, то стоит преследовать ее
        if (focusTarget == null)
        {
            StopTheAction(enemyAI);
            enemyAI.EnemyAIStateMachine = enemyAI.EnemyStatePatrolling;
            enemyAI.EnemyAIStateMachine.SeekingBattle(enemyAI);
        }
        else
        {
            if (fightingRoutine == null)
            {
                fightingRoutine = StartCoroutine(FightingEnumerator(enemyAI));
            }
        }
    }


    private IEnumerator FightingEnumerator(EnemyAI enemyAI)
    {
        //Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;

        for (float timerCounter = timer; timerCounter > 0f && focusTarget != null; timerCounter -= Time.deltaTime)
        {
            enemyAI.EnemyPresenter.EnemyCombat.GetEnemyFightingLogic(focusTarget); // Пробуем атаковать новую цель

            yield return null;
        }

        fightingRoutine = null;
        //Когда закончим, вызвать метод, говорящее о том, что мы закончили
        enemyAI.DecideWhatToDo();
    }


    public override Vector2 GetInputVector(EnemyAI enemyAI)
    {
        return enemyAI.EnemyPresenter.EnemyCombat.EnemyAttackBehavior.GetMovingVectorOnFighting(focusTarget);
    }
}
