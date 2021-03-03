using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy6 : Entity
{
    public E6_IdleState idleState;
    public E6_MoveState moveState;
    public E6_HurtState hurtState;
    public E6_DeadState deadState;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] private D_DeadState deadStateData;

    public override void Start()
    {
        base.Start();
        moveState = new E6_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E6_IdleState(this, stateMachine, "idle", idleStateData, this);
        hurtState = new E6_HurtState(this, stateMachine, "hurt", hurtStateData, this);
        deadState = new E6_DeadState(this, stateMachine, "dead", deadStateData, this);
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
