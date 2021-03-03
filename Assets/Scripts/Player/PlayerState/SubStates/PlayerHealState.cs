using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerAbilityState
{
    protected bool healInput;
    protected float startTimeCounter;
    public PlayerHealState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();
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
        player.effect.StartHealEffect();
        startTimeCounter = Time.time;
        if (!player.teleportState.canTeleport)
        {
            player.soulward.StopFollow();
        }

    }

    public override void Exit()
    {
        base.Exit();
        player.effect.StopHealEffect();
        if (!player.teleportState.canTeleport)
        {
            player.soulward.ContinueFollow();
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        healInput = player.inputHandler.healInput;
        if (!isExitingState)
        {
            if (!healInput)
            {
                isAbilityDone = true;
            }
            else if (DataGlobe.instance.playerAttributeData.curHitPoint == DataGlobe.instance.playerAttributeData.maxHitPoint || DataGlobe.instance.playerAttributeData.curManaPoint <= 0)
            {
                player.inputHandler.UseHealInput();
                isAbilityDone = true;
            }
            else if (Time.time > startTimeCounter + playerData.timeBetweenHeal)
            {

                startTimeCounter = Time.time;
                player.effect.TriggerHealEffect();
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckCanHeal()
    {
        return DataGlobe.instance.playerAttributeData.curHitPoint < DataGlobe.instance.playerAttributeData.maxHitPoint && DataGlobe.instance.playerAttributeData.curManaPoint >= playerData.manaUseHeal;
    }
}
