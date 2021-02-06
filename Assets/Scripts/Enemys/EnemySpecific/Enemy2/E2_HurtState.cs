using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_HurtState : HurtState
{
    private Enemy2 enemy;
    private int knockBackDirection;
    public E2_HurtState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_HurtState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        if (isHurtTimeOver)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public void SetKnockBackDirection(bool isRight)
    {
        knockBackDirection = isRight ? 1 : -1;
    }
}
