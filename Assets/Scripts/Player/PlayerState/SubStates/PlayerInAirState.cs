using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    protected int inputX;
    protected bool jumpInput;
    protected bool dashInput;
    protected bool changeSoulwardInput;
    protected bool castInput;
    protected bool attackInput;

    protected bool jumpInputStop;
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool oldIsTouchingWall;
    protected bool wallJumpCoyoteTime;
    protected bool coyoteTime;
    protected bool isJumping;
    protected bool soulwardHitObject;
    protected bool isInWater;
    protected bool isTouchingInteractionItem;



    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        oldIsTouchingWall = isTouchingWall;
        isGrounded = player.CheckIfGrounded();
        isTouchingWall = player.CheckIfTouchingWall();
        isInWater = player.CheckIfInWarter();
        isTouchingInteractionItem = player.CheckIfTouchingInteractionItem();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();
        inputX = player.inputHandler.normalizeInputX;
        jumpInput = player.inputHandler.jumpInput;
        jumpInputStop = player.inputHandler.jumpInputStop;
        dashInput = player.inputHandler.dashInput;
        changeSoulwardInput = player.inputHandler.changeSoulwardInput;
        castInput = player.inputHandler.castAbilityInput;
        attackInput = player.inputHandler.attackInput;
        CheckJumpMultiplier();
        if (soulwardHitObject)
        {
            soulwardHitObject = false;
            player.teleportState.ResetCanAim();
        }
        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.idleState);
        }
        else if (isInWater && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.swimState);
        }
        else if (attackInput && player.attackState.CheckCanAttack())
        {
            stateMachine.ChangeState(player.attackState);
        }
        else if (castInput)
        {
            if (player.burstState.CheckCanCast())
            {
                stateMachine.ChangeState(player.burstState);
            }
            else if (player.glideState.CheckCanCast())
            {
                stateMachine.ChangeState(player.glideState);
            }
            else if (player.teleportState.CheckCanCast())
            {
                stateMachine.ChangeState(player.teleportState);
            }
            else if (player.arrowState.CheckCanCast())
            {
                stateMachine.ChangeState(player.arrowState);
            }
        }
        else if (changeSoulwardInput && player.changeSoulwardState.CheckCanChangeSoulwardType())
        {
            stateMachine.ChangeState(player.changeSoulwardState);
        }
        else if (dashInput && player.dashState.CheckCanDash())
        {
            stateMachine.ChangeState(player.dashState);
        }
        else if (jumpInput && (isTouchingWall /*|| wallJumpCoyoteTime*/))
        {
            StopWallJumpCoyoteTime();
            if (isTouchingWall)
            {
                player.wallJumpState.SetWallJumpDirection(-player.facingDirection);
            }
            else
            {
                player.wallJumpState.SetWallJumpDirection(player.facingDirection);
            }
            stateMachine.ChangeState(player.wallJumpState);
        }
        else if (jumpInput && player.jumpState.CanJump())
        {            
            stateMachine.ChangeState(player.jumpState);
        }
        else if (jumpInput && player.wingJumpState.CanCast() )
        {
            stateMachine.ChangeState(player.wingJumpState);
        }
        else if (isTouchingWall && inputX == player.facingDirection && player.currentVelocity.y < 0.01f)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        else
        {
            player.CheckFlip(inputX);
            player.SetVelocityX(playerData.movementVelocity * inputX);

            player.anim.SetFloat("velocityY", player.currentVelocity.y);
        }
    }
    public void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                player.SetVelocityY(player.currentVelocity.y * playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (player.currentVelocity.y <= 0f)
            {
                isJumping = false;
            }
        }
    }
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void CheckCoyoteTime()
    {
        if (coyoteTime && Time.time > startTime + playerData.coyoteTime)
        {
            coyoteTime = false;

            player.jumpState.DecreaseAmountOfJump();
        }
    }
    public void CheckWallJumpCoyoteTime()
    {
        if (wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + playerData.wallJumpCoyoteTime)
        {
            wallJumpCoyoteTime = false;
            //player.jumpState.DecreaseAmountOfJump();
        }
    }
    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
    }
    public void StopWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = false;
    }
    public void StartCoyoteTime()
    {
        coyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void SetIsJumping()
    {
        isJumping = true;
    }
    public void SetSoulwardHitObject()
    {
        soulwardHitObject = true;
    }
}
