using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWingJumpState : PlayerAbilityState
{
    public PlayerWingJumpState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        player.effect.TriggerJumpEffect();
        player.anim.SetBool("canWingJump", true);
        player.inputHandler.UseJumpInput();
        player.SetVelocityY(playerData.wingJumpVelocity);
        player.inAirState.SetIsJumping();
        isAbilityDone = true;
        player.jumpState.DecreaseAmountOfJump();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CanCast()
    {
        return player.jumpState.amountOfJumpLeft == 0 && playerData.soulwardType == 1;
    }
}
