using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookState : PlayerState
{
    protected bool isStartTimeHold;
    protected float startTimeHold;
    protected int inputY;
    public PlayerLookState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        isStartTimeHold = false;
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
    }

    public override void Exit()
    {
        base.Exit();
        ResetTimeHold();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        inputY = player.inputHandler.normalizeInputY;
        player.anim.SetFloat("velocityY", inputY);
        if (inputY == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckCanLook()
    {
        return Time.time > startTimeHold + playerData.timeHoldInput && isStartTimeHold;
    }
    public void ResetTimeHold()
    {
        isStartTimeHold = false;
    }
    public void StartTimeHold()
    {
        if (!isStartTimeHold)
        {
            isStartTimeHold = true;
            startTimeHold = Time.time;
        }
    }
}
