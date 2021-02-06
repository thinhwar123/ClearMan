using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_PlayerDetectedState : PlayerDetectedState
{
    private Enemy1 enemy;
    public E1_PlayerDetectedState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_PlayerDetectedState stateData, Enemy1 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {

        base.Enter();
        entity.SetVelocity(0);
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
            if (preformCloseRangeAction)
            {
                stateMachine.ChangeState(enemy.meleeAttackState);
            }
            else if (isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.chargeState);
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
