using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    protected int inputY;
    protected bool openBookInput;
    protected bool saveInput;


    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityX(0);
        player.lookState.ResetTimeHold();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        inputY = player.inputHandler.normalizeInputY;
        openBookInput = player.inputHandler.openBookInput;
        saveInput = player.inputHandler.saveInput;

        if (!isExitingState)
        {
            if (inputX != 0)
            {
                stateMachine.ChangeState(player.moveState);
            }
            else if (inputY != 0)
            {
                player.lookState.StartTimeHold();
                if (isTouchingInteractionItem && inputY == -1)
                {
                    player.inputHandler.UseInteractInput();
                    player.curInteractionItem.Interact();
                }
                else if (player.lookState.CheckCanLook())
                {
                    stateMachine.ChangeState(player.lookState);
                }
            }
            else if (openBookInput)
            {
                stateMachine.ChangeState(player.openBookState);
            }
            else if (saveInput)
            {
                stateMachine.ChangeState(player.saveState);
            }

        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
