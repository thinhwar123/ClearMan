using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_LookForPlayerState : LookForPlayerState
{
    private Enemy3 enemy;
    public E3_LookForPlayerState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange && !isDetectingWall && isDetectingLedge)
        {

        }
        else if (isAllTurnsTimeDone)
        {

        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
