using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    protected float oldEndTimeDash;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        oldEndTimeDash = 0;
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
        player.effect.StartDashEffect();
        player.inputHandler.UseDashInput();
        player.SetGravityScale(0);
        player.SetVelocityY(0);
        player.SetVelocityX(playerData.dashVelocity * player.facingDirection);
    }

    public override void Exit()
    {
        base.Exit();
        player.effect.StopDashEffect();
        player.SetGravityScale(-1);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + playerData.dashTime)
        {
            isAbilityDone = true;
            oldEndTimeDash = Time.time;
        }
    }
    public bool CheckCanDash()
    {
        return Time.time > oldEndTimeDash + playerData.delayBetweenDash;
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
