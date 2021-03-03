using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGlideState : PlayerAbilityState
{
    protected bool isInWindArea;
    protected bool isEndWindArea;
    protected bool castInput;
    public PlayerGlideState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        isInWindArea = player.CheckIfInWindArea();
        isEndWindArea = player.CheckIfEndWindArea();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetGravityScale(0);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetGravityScale(-1);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            castInput = player.inputHandler.castAbilityInput;

            player.anim.SetFloat("velocityY", player.currentVelocity.y);
            if (castInput)
            {
                if (isGrounded)
                {
                    isAbilityDone = true;
                    player.inputHandler.UseCastAbilityInput();
                }
                else if (isInWindArea)
                {
                    player.CheckFlip(inputX);
                    player.SetVelocity(playerData.movementVelocity * inputX, playerData.glideVelocity);

                }
                else if (isEndWindArea)
                {
                    Debug.Log("end");
                    player.CheckFlip(inputX);
                    player.SetVelocity(playerData.movementVelocity * inputX, 0);
                }
                else
                {
                    player.CheckFlip(inputX);
                    player.SetVelocity(playerData.movementVelocity * inputX, -playerData.glideVelocity);
                }
            }
            else
            {
                isAbilityDone = true;
            }
        }

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public bool CheckCanCast()
    {
        return playerData.soulwardType == 1;
    }
}
