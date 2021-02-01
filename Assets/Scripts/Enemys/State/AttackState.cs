using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    protected Transform attackPosition;
    protected bool isAnimationFinish;
    protected bool isPlayerInMinAgroRange;
    public AttackState(Entity entity, FinteStateMachine stateMachine, string animBoolName, Transform attackPosition) : base(entity, stateMachine, animBoolName)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        entity.atsm.attackState = this;
        isAnimationFinish = false;
        entity.SetVelocity(0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public virtual void TriggerAttack()
    {

    }
    public virtual void FinishAttack()
    {
        isAnimationFinish = true;
    }
}
