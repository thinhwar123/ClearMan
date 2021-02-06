using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_IdleState : IdleState
{
    private Enemy4 enemy;

    public E4_IdleState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
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
        if (isIdleTimeOver)
        {
            enemy.moveState.setCharge(false);
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
