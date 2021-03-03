using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwimState : PlayerState
{
    protected int inputX;
    protected int inputY;
    protected bool jumpInput;
    protected bool changeSoulwardInput;
    public PlayerSwimState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

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
        jumpInput = player.inputHandler.jumpInput;
        changeSoulwardInput = player.inputHandler.changeSoulwardInput;
        inputY = player.inputHandler.normalizeInputY;
        inputX = player.inputHandler.normalizeInputX;
        player.CheckFlip(inputX);
        player.anim.SetFloat("velocityX", Mathf.Abs(inputX));
        player.SetVelocityX(playerData.swimVelocity * inputX);
        if (jumpInput && player.jumpState.CanJump())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        else if (changeSoulwardInput && player.changeSoulwardState.CheckCanChangeSoulwardType())
        {
            stateMachine.ChangeState(player.changeSoulwardState);
        }
        else if (inputY == -1 && player.bubbleState.CheckCanCast())
        {
            stateMachine.ChangeState(player.bubbleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
