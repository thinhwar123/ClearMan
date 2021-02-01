using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Entity
{
    public E1_IdleState idleState;
    public E1_MoveState moveState;
    public E1_PlayerDetectedState playerDetectedState;
    public E1_ChargeState chargeState;
    public E1_LookForPlayerState lookForPlayerState;
    public E1_MeleeAttackState meleeAttackState;
    public E1_HurtState hurtState;

    [SerializeField] private D_IdleState idleStateData;
    [SerializeField] private D_MoveState moveStateData;
    [SerializeField] private D_PlayerDetectedState playerDetectedStateData;
    [SerializeField] private D_ChargeState chargeStateData;
    [SerializeField] private D_LookForPlayerState lookForPlayerStateData;
    [SerializeField] private D_MeleeAttackState meleeAttackStateData;
    [SerializeField] private D_HurtState hurtStateData;
    [SerializeField] private Transform meleeAttackPosition;

    public override void Start()
    {
        base.Start();
        moveState = new E1_MoveState(this, stateMachine, "move", moveStateData, this);
        idleState = new E1_IdleState(this, stateMachine, "idle", idleStateData, this);
        playerDetectedState = new E1_PlayerDetectedState(this, stateMachine, "playerDetected", playerDetectedStateData, this);
        chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
        lookForPlayerState = new E1_LookForPlayerState(this, stateMachine, "lookForPlayer", lookForPlayerStateData, this);
        meleeAttackState = new E1_MeleeAttackState(this, stateMachine, "melleAttack", meleeAttackPosition, meleeAttackStateData, this);
        hurtState = new E1_HurtState(this, stateMachine, "hurt", hurtStateData, this);
        stateMachine.Initialize(idleState);
    }
    public override void TakeDame(AttackDetails attackDetails)
    {
        base.TakeDame(attackDetails);
        hurtState.SetKnockBackDirection(aliveGO.transform.position.x > attackDetails.position.x);
        stateMachine.ChangeState(hurtState);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.attackRadius);
        Gizmos.DrawLine(meleeAttackPosition.position, meleeAttackPosition.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeAction));
    }
}
