using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_HurtState : HurtState
{
    private Enemy4 enemy;
    private int knockBackDirection;
    public E4_HurtState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_HurtState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
            //if (preformCloseRangeAction)
            //{
            //    stateMachine.ChangeState(enemy.meleeAttackState);
            //}
            //else 
            if (isPlayerInMaxAgroRange)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
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
    public void SetKnockBackDirection(bool isRight)
    {
        knockBackDirection = isRight ? 1 : -1;
    }
}
