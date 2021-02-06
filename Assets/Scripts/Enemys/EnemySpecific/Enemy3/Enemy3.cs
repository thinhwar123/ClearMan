using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Entity
{
    public E3_IdleState idleState;
    public E3_MoveState moveState;
    public E3_PlayerDetectedState playerDetectedState;
    public E3_ChargeState chargeState;
    public E3_HurtState hurtState;
    public E3_DeadState deadState;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] private D_DeadState deadStateData;

    public Collider2D player;

    public override void Start()
    {
        base.Start();
        moveState = new E3_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E3_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E3_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new E3_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        hurtState = new E3_HurtState(this, stateMachine, "hurt", hurtStateData, this);
        deadState = new E3_DeadState(this, stateMachine, "dead", deadStateData, this);
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
            hurtState.SetKnockBackDirection(attackDetails.position);
            stateMachine.ChangeState(hurtState);
        }
    }
    public override void OnHitGround(Collision2D collision)
    {
        base.OnHitGround(collision);
        if (stateMachine.currentState != chargeState)
        {
            Debug.Log("Hit");
            moveState.SetMoveDirection(collision);
            stateMachine.ChangeState(moveState);
        }

    }
    public override void OnHitPlayer()
    {
        base.OnHitPlayer();
        stateMachine.ChangeState(playerDetectedState);
    }
    public override void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.DrawWireSphere(wallCheck.position, entityData.wallCheckDistance);
            Gizmos.DrawWireSphere(playerCheck.position, entityData.minAgroDistance);
            Gizmos.DrawWireSphere(playerCheck.position, entityData.maxAgroDistance);
        }

    }

    public override bool CheckWall()
    {
        return Physics2D.OverlapCircle(wallCheck.position, entityData.wallCheckDistance, entityData.whatIsWall);
    }

    public override bool CheckPlayerInMinAgroRange()
    {
        if (Physics2D.OverlapCircle(playerCheck.position, entityData.minAgroDistance, entityData.whatIsPlayer))
        {
            player = Physics2D.OverlapCircle(playerCheck.position, entityData.minAgroDistance, entityData.whatIsPlayer);
        }        
        return Physics2D.OverlapCircle(playerCheck.position, entityData.minAgroDistance, entityData.whatIsPlayer);
    }

    public override bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.OverlapCircle(playerCheck.position, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }

}
