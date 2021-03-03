using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_ChargeState : ChargeState
{
    private Enemy3 enemy;
    
    public E3_ChargeState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_ChargeState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        Vector2 dir = (enemy.player.transform.position - enemy.transform.position).normalized;
        enemy.Flip(dir.x > 0 ? 1 : -1);
        enemy.SetVelocity(stateData.chargeSpeed, dir, true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isChargeTimeOver)
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
}
