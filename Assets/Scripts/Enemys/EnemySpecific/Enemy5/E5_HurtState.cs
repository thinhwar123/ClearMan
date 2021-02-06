using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E5_HurtState : HurtState
{
    private Enemy5 enemy;
    private int knockBackDirection;
    public E5_HurtState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_HurtState stateData, Enemy5 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        entity.SetVelocity(stateData.knockBackSpeed, new Vector2(knockBackDirection, 2));
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
