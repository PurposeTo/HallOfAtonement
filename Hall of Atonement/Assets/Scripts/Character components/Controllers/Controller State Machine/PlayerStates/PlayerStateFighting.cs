using System.Collections;
using UnityEngine;

public class PlayerStateFighting : PlayerStateMachine
{
    private Coroutine fightingRoutine;

    private float timer = 0.5f;


    public override void Fighting(PlayerController playerController)
    {
        if (fightingRoutine == null)
        {
            fightingRoutine = StartCoroutine(FightingEnumerator(playerController));
        }
    }


    public override void Patrolling(PlayerController playerController)
    {
        StopTheAction(playerController);

        playerController.PlayerStateMachine = playerController.PlayerStatePatrolling;
        playerController.PlayerStateMachine.Patrolling(playerController);
    }


    private IEnumerator FightingEnumerator(PlayerController playerController)
    {
        // Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;

        while (true)
        {
            playerController.CharacterPresenter.Combat.SetTargetToAttack(playerController.CharacterPresenter.CharacterType.SearchingTarget());


            if (playerController.CharacterPresenter.Combat.GetTargetToAttack() != null) // Если цель найдена
            {
                // Атаковать цель, пока та доступна
                float timerCounter = timer;

                while (timerCounter > 0f && playerController.CharacterPresenter.Combat.GetTargetToAttack() != null)
                {
                    playerController.PlayerPresenter.PlayerCombat.GetPlayerFightingLogic();

                    yield return null;
                    timerCounter -= Time.deltaTime;
                }
            }
            else
            {
                playerController.PlayerPresenter.PlayerCombat.GetPlayerFightingLogic();
                // Подождать кадр прежде чем искать цель
                yield return null;
            }
        }
    }


    private protected override void StopTheAction(PlayerController playerController)
    {
        if (fightingRoutine != null)
        {
            StopCoroutine(fightingRoutine);
            fightingRoutine = null;
        }
        playerController.CharacterPresenter.Combat.SetTargetToAttack(null);
    }
}
