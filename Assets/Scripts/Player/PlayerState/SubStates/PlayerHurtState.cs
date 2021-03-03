using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerAbilityState
{
    protected AttackDetails attackDetails;
    protected float lastTimeHurt;
    public PlayerHurtState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        lastTimeHurt = -playerData.unavailableTime;
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();
        isAbilityDone = true;
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = playerData.hurtTimeScale;
        player.SetVelocity(playerData.pushBackForce, playerData.angle, attackDetails.position.x < player.transform.position.x ? 1 : -1);
        player.StartCoroutine(player.Unvailable());

    }

    public override void Exit()
    {
        base.Exit();
        lastTimeHurt = startTime;
        player.effect.TriggerHurtEffect();
        Time.timeScale = 1;
        player.SetVelocity(0, 0);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckCanHurt()
    {
        return Time.time > lastTimeHurt + playerData.unavailableTime;
    }
    public void SetAttackDetails(AttackDetails attackDetails)
    {
        this.attackDetails = attackDetails;
    }
}
