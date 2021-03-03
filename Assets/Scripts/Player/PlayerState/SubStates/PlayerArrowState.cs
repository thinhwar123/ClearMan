using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowState : PlayerAbilityState
{
    protected bool castInput;

    protected float startAimTime;
    public PlayerArrowState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        Time.timeScale = playerData.arrowTimeScale;
        startAimTime = Time.realtimeSinceStartup;
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;
        player.ResetSoulwardPosition();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            castInput = player.inputHandler.castAbilityInput;

            if (Time.realtimeSinceStartup > startAimTime + playerData.aimTimeArrow)
            {
                isAbilityDone = true;
                player.inputHandler.UseCastAbilityInput();
                ShootSoulward();
            }
            else if (castInput)
            {
                ChangeAimAngle();
            }
            else
            {
                isAbilityDone = true;
                ShootSoulward();
            }
        }


    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckCanCast()
    {
        return playerData.soulwardType == 5;
    }
    public Vector3 GetMousePosition()
    {
        Vector3 res = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(res.x, res.y, 0);
    }
    public Vector3 GetDirectionAim()
    {
        return GetMousePosition() - player.transform.position;
    }
    public void ChangeAimAngle()
    {
        Vector3 aimDirection = GetDirectionAim();
        if (aimDirection.x != 0)
        {
            player.CheckFlip(aimDirection.x > 0 ? 1 : -1);
        }
        player.soulward.Aim(aimDirection, 5, playerData.aimTimeArrow);
        player.anim.SetFloat("aimDirectionY", aimDirection.normalized.y);
    }
    public void ShootSoulward()
    {
        player.soulward.Shoot(GetDirectionAim(), 5);

    }

}
