using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOpenBookState : PlayerState
{
    protected bool openBookInput;
    protected bool isOpenBook;
    protected bool isOpenBookDone;
    public PlayerOpenBookState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        isOpenBook = false;
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();
        if (isOpenBook)
        {
            isOpenBookDone = true;
        }
        else
        {
            stateMachine.ChangeState(player.idleState);
            player.inputHandler.ChangeActionMap("Gameplay");
        }
        
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        if (isOpenBook)
        {

            DataGlobe.instance.systemUI.OpenBook();
        }
        else
        {
            DataGlobe.instance.systemUI.CloseBook();
        }
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        Time.timeScale = 0;
        player.anim.updateMode = AnimatorUpdateMode.UnscaledTime;
        base.Enter();

        player.inputHandler.UseOpenBookInput();
        player.inputHandler.ChangeActionMap("UI");
        isOpenBook = true;
        isOpenBookDone = false;

    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("endOpenBook", false);
        player.anim.updateMode = AnimatorUpdateMode.Normal;
        Time.timeScale = 1;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (isOpenBook)
        {
            openBookInput = player.inputHandler.openBookInput;
            if (openBookInput && isOpenBookDone)
            {
                isOpenBook = false;
                player.anim.SetBool("endOpenBook", true);
                player.anim.SetBool("openBook", false);
                player.inputHandler.UseOpenBookInput();
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
