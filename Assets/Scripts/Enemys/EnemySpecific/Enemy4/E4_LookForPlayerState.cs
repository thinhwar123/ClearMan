using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_LookForPlayerState : LookForPlayerState
{
    private Enemy4 enemy;
    public E4_LookForPlayerState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_LookForPlayerState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
        else if (isAllTurnsTimeDone)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
