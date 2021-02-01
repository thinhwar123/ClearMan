using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeState : State
{
    protected D_ChargeState stateData;
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingWall;
    protected bool isDetectingLegde;
    protected bool isChargeTimeOver;
    protected bool preformCloseRangeAction;
    public ChargeState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_ChargeState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();        
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingWall = entity.CheckWall();
        isDetectingLegde = entity.CheckLedge();
        preformCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.chargeSpeed);
        isChargeTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (Time.time >= startTime + stateData.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
