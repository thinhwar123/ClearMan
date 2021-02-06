using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_PlayerDetectedState : PlayerDetectedState
{
    private Enemy4 enemy;
    public E4_PlayerDetectedState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.SetVelocity(0);
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
                enemy.moveState.setCharge(true);
                stateMachine.ChangeState(enemy.moveState);
            }
            else if (!isPlayerInMaxAgroRange)
            {
                enemy.lookForPlayerState.SetTurnImediately(true);
                stateMachine.ChangeState(enemy.lookForPlayerState);
            }
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
