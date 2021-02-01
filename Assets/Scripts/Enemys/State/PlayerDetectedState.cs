using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectedState : State
{
    protected D_PlayerDetectedState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool preformLongRangeAction; // het thoi gian quyet dinh (ko phai bien check)
    protected bool preformCloseRangeAction;

    public PlayerDetectedState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;

    }

    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();

        preformCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(0);
        preformLongRangeAction = false;
}

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.longRangeActionTime)
        {
            preformLongRangeAction = true;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
