using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : AttackState
{
    protected D_MeleeAttackState stateData;
    protected AttackDetails attackDetails;
    public MeleeAttack(Entity entity, FinteStateMachine stateMachine, string animBoolName, Transform attackPosition, D_MeleeAttackState stateData) : base(entity, stateMachine, animBoolName, attackPosition)
    {
        this.stateData = stateData;
    }

    public override void DoCheck()
    {
        base.DoCheck();
    }

    public override void Enter()
    {
        base.Enter();
        attackDetails.attackDamage = stateData.attackDamage;
        attackDetails.position = entity.aliveGO.transform.position;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FinishAttack()
    {
        base.FinishAttack();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public override void TriggerAttack()
    {
        base.TriggerAttack();

        //Collider2D[] detectedObjects = Physics2D.OverlapCircleAll(attackPosition.position, stateData.attackRadius, stateData.whatIsPlayer);
        //attackDetails.position = attackPosition.position;
        //foreach (Collider2D coll in detectedObjects)
        //{
        //    coll.transform.SendMessage("TakeDame", attackDetails );
        //}
    }
}
