using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBubbleState : PlayerState
{
    protected int inputX;
    protected int inputY;
    protected bool isInDeepWater;
    protected bool isInWater;
    public PlayerBubbleState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        isInDeepWater = player.CheckIfInDeepWater();
        isInWater = player.CheckIfInWarter();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravityScale(0);
        player.SetWaterCheckTrigger(true);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetGravityScale(-1);
        player.SetWaterCheckTrigger(false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        inputX = player.inputHandler.normalizeInputX;
        inputY = player.inputHandler.normalizeInputY;
        if (inputY >= 0 && !isInDeepWater && isInWater)
        {
            player.SetVelocityY(0);
            stateMachine.ChangeState(player.swimState);
        }
        else
        {
            player.CheckFlip(inputX);
            player.SetVelocity(inputX * playerData.bubbleVelocityX, playerData.upForce + inputY * playerData.downForce);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckCanCast()
    {
        return playerData.soulwardType == 4;
    }
}
