using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBurstState : PlayerAbilityState
{
    protected float lastCastTime;
    public PlayerBurstState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        lastCastTime = 0;
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
        player.effect.TriggerExplosionEffect();
        lastCastTime = Time.time;
        player.inputHandler.UseCastAbilityInput();
        RaycastHit2D[] hit = Physics2D.CircleCastAll(player.transform.position, playerData.burstRange, Vector2.zero, 0, playerData.whatIsWeakObject);
        if (hit != null)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                hit[i].collider.GetComponent<WeakObject>().DestroyObject();
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckCanCast()
    {
        return playerData.soulwardType == 0 && Time.time > lastCastTime + playerData.burstDelayTime;
    }
}
