using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E3_MoveState : MoveState
{
    protected Enemy3 enemy;
    protected Vector3 moveDirection;
    protected Vector3 lastDirection;
    public E3_MoveState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy3 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.Flip(moveDirection.x > 0 ? 1 : -1);
        enemy.SetVelocity(stateData.movementSpeed, moveDirection, true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isPlayerInMinAgroRange)
        {
            stateMachine.ChangeState(enemy.playerDetectedState);
        }
    }
    public void SetMoveDirection(Vector2 moveDirection)
    {
        lastDirection = this.moveDirection;
        this.moveDirection = moveDirection;
    }
    public void SetLastDirection(Vector2 lastDirection)
    {
        this.lastDirection = lastDirection;

    }
    public void SetMoveDirection(Collision2D collision)
    {
        lastDirection = this.moveDirection;
        moveDirection = Vector2.Reflect(lastDirection.normalized, collision.contacts[0].normal);
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
