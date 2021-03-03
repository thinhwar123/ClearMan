using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveObjectState : PlayerAbilityState
{
    protected bool isTouchingPhysicalObject;
    protected bool moveObjectInput;
    protected RaycastHit2D physicalObject;
    public PlayerMoveObjectState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
        isTouchingPhysicalObject = player.CheckIfTouchingPhysicalObject();
    }

    public override void Enter()
    {
        base.Enter();
        physicalObject = Physics2D.Raycast(player.transform.position,  Vector2.right * player.facingDirection, playerData.physicalObjectCheckDistance, playerData.whatIsPhysicalObject);
    }

    public override void Exit()
    {
        base.Exit();
        player.anim.SetFloat("moveDirection", 0);
        physicalObject.collider.GetComponent<Rigidbody2D>().velocity = new Vector2(0, physicalObject.collider.GetComponent<Rigidbody2D>().velocity.y);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        moveObjectInput = player.inputHandler.moveObjectInput;
        if (!isExitingState)
        {
            if (!isGrounded || !isTouchingPhysicalObject)
            {
                isAbilityDone = true;
                player.inputHandler.UseMoveObjectInput();
            }
            else if (moveObjectInput)
            {
                player.anim.SetFloat("velocityX", inputX * player.facingDirection);
                if (inputX != 0)
                {
                    player.anim.SetFloat("moveDirection", inputX * player.facingDirection);
                }
                player.SetVelocityX(playerData.moveObjectVelocity * inputX);
                physicalObject.collider.GetComponent<Rigidbody2D>().velocity = new Vector2(playerData.moveObjectVelocity * inputX, physicalObject.collider.GetComponent<Rigidbody2D>().velocity.y);
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
}
