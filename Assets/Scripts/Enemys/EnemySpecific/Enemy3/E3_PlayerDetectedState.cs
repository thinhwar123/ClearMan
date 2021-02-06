using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_PlayerDetectedState : PlayerDetectedState
{
    private Enemy3 enemy;
    public E3_PlayerDetectedState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {

        base.Enter();
        enemy.SetVelocity(0, Vector2.zero, true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (preformLongRangeAction)
        {
            if (isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.chargeState);
            }        
            else if (!isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
