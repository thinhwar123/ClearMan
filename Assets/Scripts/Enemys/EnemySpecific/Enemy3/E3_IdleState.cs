using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_IdleState : IdleState
{
    private Enemy3 enemy;

    public E3_IdleState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_IdleState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(0f);
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
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            enemy.moveState.SetMoveDirection(dir);
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
