using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Entity
{
    public E2_IdleState idleState;
    public E2_MoveState moveState;
    public E2_HurtState hurtState;
    public E2_DeadState deadState;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] private D_DeadState deadStateData;

    public bool checkWall;
    public bool checkLedge;
    public override void Start()
    {
        base.Start();
        moveState = new E2_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E2_IdleState(this, stateMachine, "idle", idleStateData, this);
        hurtState = new E2_HurtState(this, stateMachine, "hurt", hurtStateData, this);
        deadState = new E2_DeadState(this, stateMachine, "dead", deadStateData, this);
        stateMachine.Initialize(idleState);
    }
    public override void TakeDame(AttackDetails attackDetails)
    {
        base.TakeDame(attackDetails);

        if (isDead)
        {
            stateMachine.ChangeState(deadState);
        }
        else
        {
            stateMachine.ChangeState(hurtState);
        }
    }
    public override void OnHitPlayer()
    {
        base.OnHitPlayer();
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (drawGizmos)
        {

        }

    }

    public override bool CheckWall()
    {
        Vector2 castDir = Vector2.right * facingDirection;
        if (aliveGO.transform.eulerAngles.z == 90)
        {
            castDir = Vector2.up * facingDirection;
        }
        else if (aliveGO.transform.eulerAngles.z == 180)
        {
            castDir = Vector2.left * facingDirection;
        }
        else if (aliveGO.transform.eulerAngles.z == 270)
        {
            castDir = Vector2.down * facingDirection;
        }
        return Physics2D.Raycast(wallCheck.position, castDir, entityData.wallCheckDistance, entityData.whatIsWall);
    }

    public override bool CheckLedge()
    {
        Vector2 castDir = Vector2.down;
        if (aliveGO.transform.eulerAngles.z == 90)
        {
            castDir = Vector2.right ;
        }
        else if (aliveGO.transform.eulerAngles.z == 180)
        {
            castDir = Vector2.up ;
        }
        else if (aliveGO.transform.eulerAngles.z == 270)
        {
            castDir = Vector2.left;
        }
        return Physics2D.Raycast(ledgeCheck.position, castDir, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        checkWall = CheckWall();
        checkLedge = CheckLedge();
    }
}
