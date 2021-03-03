using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleportState : PlayerAbilityState
{
    protected bool castInput;

    public bool canAim { get; private set; }
    public bool canTeleport { get; private set; }

    protected float startAimTime;

    public PlayerTeleportState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
        canAim = true;
        canTeleport = false;
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();
        player.anim.SetBool("teleport", false);
        isAbilityDone = true;
        player.ResetSoulwardPosition();
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("HitObject"), false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        player.transform.position = player.soulward.transform.position;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0, 0);
        if (canAim)
        {
            Time.timeScale = playerData.teleportTimeScale;
            startAimTime = Time.realtimeSinceStartup;
        }
        else if (canTeleport)
        {
            player.anim.SetBool("aim", false);
            player.anim.SetBool("teleport", true);
            player.inputHandler.UseCastAbilityInput();
            player.inputHandler.DelayCastInput(playerData.delayTimeTeleport);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("HitObject"), true);
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            castInput = player.inputHandler.castAbilityInput;
            if (canAim)
            {
                if (Time.realtimeSinceStartup > startAimTime + playerData.aimTimeTeleport)
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

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public bool CheckCanCast()
    {
        return (canAim || (canTeleport && player.soulward.canTeleport) )&& playerData.soulwardType == 3;
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
            player.CheckFlip(aimDirection.x > 0 ? 1: -1);
        }
        player.soulward.Aim(aimDirection, 3, playerData.aimTimeTeleport);
        player.anim.SetFloat("aimDirectionY", aimDirection.normalized.y);
    }
    public void ShootSoulward()
    {
        player.soulward.Shoot(GetDirectionAim(), 3);
        canAim = false;
        canTeleport = true;
    }
    public void SetCanTeleport(bool canTeleport) 
    {
        this.canTeleport = canTeleport;
    }
    public void ResetCanAim()
    {
        if (!canTeleport && !canAim)
        {
            canAim = true;
        }
    }
}
