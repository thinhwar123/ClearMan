using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtState : State
{
    protected D_HurtState stateData;
    protected bool isHurtTimeOver;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool preformCloseRangeAction;
    public HurtState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_HurtState stateData) : base(entity, stateMachine, animBoolName)
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
        isHurtTimeOver = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (Time.time >= startTime + stateData.duration)
        {
            isHurtTimeOver = true;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
