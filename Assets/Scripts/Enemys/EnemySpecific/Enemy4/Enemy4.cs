using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Entity
{
    public E4_IdleState idleState;
    public E4_MoveState moveState;
    public E4_PlayerDetectedState playerDetectedState;
    public E4_LookForPlayerState lookForPlayerState;
    public E4_HurtState hurtState;
    public E4_DeadState deadState;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] private D_DeadState deadStateData;

    public override void Start()
    {
        base.Start();
        moveState = new E4_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E4_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E4_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        lookForPlayerState = new E4_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        hurtState = new E4_HurtState(this, stateMachine, "hurt", hurtStateData, this);
        deadState = new E4_DeadState(this, stateMachine, "dead", deadStateData, this);
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
            hurtState.SetKnockBackDirection(aliveGO.transform.position.x > attackDetails.position.x);
            stateMachine.ChangeState(hurtState);
        }
    }
    public override void OnHitPlayer()
    {
        base.OnHitPlayer();
        stateMachine.ChangeState(playerDetectedState);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (drawGizmos)
        {

        }

    }
}
