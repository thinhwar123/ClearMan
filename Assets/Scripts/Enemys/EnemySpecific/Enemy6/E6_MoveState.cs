using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_MoveState : MoveState
{
    protected Enemy6 enemy;
    public E6_MoveState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy6 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        entity.SetVelocity(stateData.movementSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlipAffterIdle(true);
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
