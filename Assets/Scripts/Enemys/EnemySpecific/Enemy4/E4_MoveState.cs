using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E4_MoveState : MoveState
{
    protected Enemy4 enemy;
    protected bool isEndJump;
    protected bool isCharge;
    public E4_MoveState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy4 enemy) : base(entity, stateMachine, animBoolName, stateData)
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
        isEndJump = false;
        entity.SetVelocity(stateData.movementSpeed, new Vector2(enemy.facingDirection, stateData.jumpForce));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishMove()
    {
        base.FinishMove();
        isEndJump = true;
        //entity.SetVelocity(0, Vector2.zero, true);
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        enemy.aliveGO.GetComponent<Animator>().SetFloat("velocityY", enemy.aliveGO.GetComponent<Rigidbody2D>().velocity.y);

        if (isEndJump )
        {
            if (isCharge && isDetectingLedge)
            {
                stateMachine.ChangeState(enemy.playerDetectedState);
            }
            else if (isDetectingWall)
            {
                enemy.idleState.SetFlipAffterIdle(true);
                stateMachine.ChangeState(enemy.idleState);
            }
            else if (isDetectingLedge)
            {
                enemy.idleState.SetFlipAffterIdle(false);
                stateMachine.ChangeState(enemy.idleState);
            }
        }

    }
    public void setCharge(bool charge)
    {
        isCharge = charge;
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void TriggerMove()
    {
        base.TriggerMove();
    }
}
