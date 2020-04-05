using System.Collections;
using UnityEngine;

public class PlayerStateFighting : PlayerControllerStateMachine
{
    private PlayerCombat playerCombat;

    private Coroutine fightingRoutine;

    private float timer = 0.5f;


    private void Start()
    {
        playerCombat = gameObject.GetComponent<PlayerCombat>();
    }


    public override void Fighting(PlayerMovement controller)
    {
        if (fightingRoutine == null)
        {
            fightingRoutine = StartCoroutine(FightingEnumerator(controller));
        }
    }


    public override void Patrolling(PlayerMovement controller)
    {
        if (fightingRoutine != null)
        {
            StopCoroutine(fightingRoutine);
            fightingRoutine = null;
        }
        controller.CharacterPresenter.Combat.targetToAttack = null;
        controller.PlayerControllerStateMachine = controller.PlayerStatePatrolling;
        controller.PlayerControllerStateMachine.Patrolling(controller);
    }


    private IEnumerator FightingEnumerator(PlayerMovement controller)
    {
        //Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;

        while (true)
        {
            playerCombat.targetToAttack = controller.CharacterPresenter.CharacterType.SearchingTarget();


            if (playerCombat.targetToAttack != null) //Если цель найдена
            {
                //Атаковать цель, пока та доступна
                float timerCounter = timer;

                while (timerCounter > 0f && IsTargetAvailable(controller))
                {
                    playerCombat.GetPlayerFightingLogic();

                    yield return null;
                    timerCounter -= Time.deltaTime;
                }
            }
            else
            {
                playerCombat.GetPlayerFightingLogic();
                //подождать кадр прежде чем искать цель
                yield return null;
            }
        }
    }




    private bool IsTargetAvailable(PlayerMovement controller)
    {
        return playerCombat.targetToAttack != null;
    }
}
