using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookForPlayerState : State
{
    protected D_LookForPlayerState stateData;

    protected bool turnImediately;
    protected bool isDetectingWall;
    protected bool isDetectingLedge;
    protected bool isPlayerInMinAgroRange;
    protected bool isAllTurnDone;
    protected bool isAllTurnsTimeDone;

    protected float lastTurnTime;
    protected int amountOfTurnDone;
    public LookForPlayerState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData) : base(entity, stateMachine, animBoolName)
    {
        this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingWall = entity.CheckWall();
        isDetectingLedge = entity.CheckLedge();
    }

    public override void Enter()
    {
        base.Enter();
        lastTurnTime = startTime;
        isAllTurnDone = false;
        isAllTurnsTimeDone = false;
        amountOfTurnDone = 0;
        entity.SetVelocity(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (turnImediately)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnDone++;
            turnImediately = false;
        }
        else if (Time.time >= lastTurnTime + stateData.timeBetweenTurn && !isAllTurnDone)
        {
            entity.Flip();
            lastTurnTime = Time.time;
            amountOfTurnDone++;
        }
        if (amountOfTurnDone >= stateData.amountOfTurn)
        {
            isAllTurnDone = true;
        }
        if (Time.time >= lastTurnTime + stateData.timeBetweenTurn && isAllTurnDone)
        {
            isAllTurnsTimeDone = true;
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public void SetTurnImediately(bool flip)
    {
        turnImediately = flip;
    }
}
