using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : Entity
{
    public E5_IdleState idleState;
    public E5_MoveState moveState;
    public E5_HurtState hurtState;
    public E5_DeadState deadState;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] private D_DeadState deadStateData;

    public override void Start()
    {
        base.Start();
        moveState = new E5_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E5_IdleState(this, stateMachine, "idle", idleStateData, this);
        hurtState = new E5_HurtState(this, stateMachine, "hurt", hurtStateData, this);
        deadState = new E5_DeadState(this, stateMachine, "dead", deadStateData, this);
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
            hurtState.SetKnockBackDirection(transform.position.x > attackDetails.position.x);
            stateMachine.ChangeState(hurtState);
        }
    }
    public override void OnHitPlayer()
    {
        base.OnHitPlayer();
        //stateMachine.ChangeState(playerDetectedState);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (drawGizmos)
        {
        }

    }
}
