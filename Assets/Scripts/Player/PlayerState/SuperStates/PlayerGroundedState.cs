using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int inputX;
    protected bool jumpInput;
    protected bool dashInput;
    protected bool changeSoulwardInput;
    protected bool castInput;
    protected bool moveObjectInput;
    protected bool attackInput;
    protected bool healInput;

    protected bool isGrounded;
    protected bool isTouchingPhysicalObject;
    protected bool isTouchingInteractionItem;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfGrounded();
        isTouchingPhysicalObject = player.CheckIfTouchingPhysicalObject();
        isTouchingInteractionItem = player.CheckIfTouchingInteractionItem();
    }

    public override void Enter()
    {
        base.Enter();
        player.jumpState.ResetAmountOfJumpLeft();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        inputX = player.inputHandler.normalizeInputX;
        dashInput = player.inputHandler.dashInput;
        jumpInput = player.inputHandler.jumpInput;
        changeSoulwardInput = player.inputHandler.changeSoulwardInput;
        castInput = player.inputHandler.castAbilityInput;
        moveObjectInput = player.inputHandler.moveObjectInput;
        attackInput = player.inputHandler.attackInput;
        healInput = player.inputHandler.healInput;

        player.teleportState.ResetCanAim();

        if (jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (moveObjectInput && isTouchingPhysicalObject)
        {
            stateMachine.ChangeState(player.moveObjectState);
        }
        else if (dashInput && player.dashState.CheckCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else if (changeSoulwardInput && player.changeSoulwardState.CheckCanChangeSoulwardType())
        {
            stateMachine.ChangeState(player.changeSoulwardState);
        }        
        else if (castInput)
        {
            if (player.burstState.CheckCanCast())
            {
                stateMachine.ChangeState(player.burstState);
            }
            else if (player.teleportState.CheckCanCast())
            {
                stateMachine.ChangeState(player.teleportState);
            }
            else if (player.arrowState.CheckCanCast())
            {
                stateMachine.ChangeState(player.arrowState);
            }
        }
        else if (attackInput)
        {
            stateMachine.ChangeState(player.attackState);
        }
        else if (healInput && player.healState.CheckCanHeal())
        {
            stateMachine.ChangeState(player.healState);
        }
        else if (!isGrounded)
        {
            player.inAirState.StartCoyoteTime();
            stateMachine.ChangeState(player.inAirState);
        } 

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
