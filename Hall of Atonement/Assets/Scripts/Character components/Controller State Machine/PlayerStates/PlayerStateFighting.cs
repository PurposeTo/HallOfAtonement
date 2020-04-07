using System.Collections;
using UnityEngine;

public class PlayerStateFighting : PlayerStateMachine
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
        StopTheAction(controller);

        controller.PlayerStateMachine = controller.PlayerStatePatrolling;
        controller.PlayerStateMachine.Patrolling(controller);
    }


    private IEnumerator FightingEnumerator(PlayerMovement movement)
    {
        // Обязательно подождать кадр, что бы избежать бага "бесконечного цикла"!
        yield return null;

        while (true)
        {
            playerCombat.targetToAttack = movement.CharacterPresenter.CharacterType.SearchingTarget();


            if (playerCombat.targetToAttack != null) // Если цель найдена
            {
                // Атаковать цель, пока та доступна
                float timerCounter = timer;

                while (timerCounter > 0f && IsTargetAvailable())
                {
                    playerCombat.GetPlayerFightingLogic();

                    yield return null;
                    timerCounter -= Time.deltaTime;
                }
            }
            else
            {
                playerCombat.GetPlayerFightingLogic();
                // Подождать кадр прежде чем искать цель
                yield return null;
            }
        }
    }


    private bool IsTargetAvailable()
    {
        return playerCombat.targetToAttack != null;
    }


    private protected override void StopTheAction(PlayerMovement playerMovement)
    {
        if (fightingRoutine != null)
        {
            StopCoroutine(fightingRoutine);
            fightingRoutine = null;
        }
        playerMovement.CharacterPresenter.Combat.targetToAttack = null;
    }
}
