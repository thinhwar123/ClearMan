using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public int amountOfJumpLeft;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        amountOfJumpLeft = playerData.amountOfJump;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.effect.TriggerJumpEffect();
        player.inputHandler.UseJumpInput();
        player.SetVelocityY(playerData.jumpVelocity);
        player.inAirState.SetIsJumping();
        isAbilityDone = true;
        amountOfJumpLeft--;
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
    public bool CanJump()
    {
        return amountOfJumpLeft > 0;
    }
    public void ResetAmountOfJumpLeft()
    {
        amountOfJumpLeft = playerData.amountOfJump;
        player.anim.SetBool("canWingJump", false);
    }
    public void DecreaseAmountOfJump()
    {
        amountOfJumpLeft--;
    }
}
