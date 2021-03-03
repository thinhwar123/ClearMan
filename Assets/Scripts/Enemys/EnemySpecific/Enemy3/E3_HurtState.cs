using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_HurtState : HurtState
{
    private Enemy3 enemy;
    private Vector2 knockBackDirection;
    public E3_HurtState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_HurtState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        entity.SetVelocity(stateData.knockBackSpeed, knockBackDirection);
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
            if (isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }
            else
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public void SetKnockBackDirection(bool isRight)
    {
        knockBackDirection = isRight ? new Vector2(1, 1) : new Vector2(-1,1);
    }
    public void SetKnockBackDirection(Vector2 attackPosition)
    {
        knockBackDirection =(Vector2)enemy.transform.position - attackPosition;
    }
}
