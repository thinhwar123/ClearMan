using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveState : PlayerState
{
    protected bool saveInput;
    protected bool isSave;
    public PlayerSaveState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        isSave = false;
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();
        stateMachine.ChangeState(player.idleState);
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
        player.inputHandler.UseSaveInput();
        isSave = true;
        DataGlobe.instance.SaveCurData();
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("endSave", false);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isSave)
        {
            saveInput = player.inputHandler.saveInput;
            if (saveInput)
            {
                player.anim.SetBool("endSave", true);
                player.anim.SetBool("save", false);
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
