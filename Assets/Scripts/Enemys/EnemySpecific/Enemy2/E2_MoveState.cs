using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_MoveState : MoveState
{
    protected Enemy2 enemy;
    protected bool canTurn;
    protected Vector2 directionMove;
    public E2_MoveState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_MoveState stateData, Enemy2 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        canTurn = false;
        enemy.SetGravity(0);
        enemy.aliveGO.GetComponent<CapsuleCollider2D>().isTrigger = true;

        if (enemy.aliveGO.transform.eulerAngles.z == 0)
        {
            directionMove = Vector2.left;
        }
        else if (enemy.aliveGO.transform.eulerAngles.z == 90)
        {
            directionMove = Vector2.down;
        }
        else if (enemy.aliveGO.transform.eulerAngles.z == 180)
        {
            directionMove = Vector2.right;
        }
        else if (enemy.aliveGO.transform.eulerAngles.z == 270)
        {
            directionMove = Vector2.up;
        }
        entity.SetVelocity(stateData.movementSpeed, directionMove, true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!canTurn && isDetectingLedge)
        {
            canTurn = true;
        }
        else if (isDetectingWall && canTurn)
        {
            canTurn = false;
            enemy.aliveGO.transform.Rotate(0, 0, -90);
            stateMachine.ChangeState(enemy.moveState);
        }
        else if (!isDetectingLedge && canTurn)
        {
            canTurn = false;
            enemy.aliveGO.transform.Rotate(0 , 0 , 90);
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
