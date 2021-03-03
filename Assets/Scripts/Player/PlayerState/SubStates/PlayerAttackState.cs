using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerAbilityState
{
    protected int inputY;
    protected BoxCollider2D attackHitBox;
    protected Collider2D[] detectedEnemy;
    protected Collider2D[] detectedBounceObject;
    protected bool canBounce;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinish()
    {
        base.AnimationFinish();
        if (canBounce)
        {
            stateMachine.ChangeState(player.bounceState);
        }
        else
        {
            isAbilityDone = true;
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        Damage();
        CheckCanBounce();
    }

    public override void DoChecks()
    {
        base.DoChecks();

    }

    public override void Enter()
    {
        base.Enter();
        inputY = player.inputHandler.normalizeInputY;
        if (isGrounded && inputY < 0)
        {
            inputY = 0;
        }
        player.anim.SetFloat("attackDirection", inputY);
        player.inputHandler.UseAttackInput();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            player.CheckFlip(inputX);
            player.SetVelocityX(playerData.movementVelocity * inputX);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public void Damage()
    {
        if (inputY == 1)
        {
            attackHitBox = player.attackUpHitBox;
        }
        else if (inputY == -1)
        {
            attackHitBox = player.attackDownHitBox;
        }
        else if (inputY == 0)
        {
            attackHitBox = player.attackHitBox;
        }
        detectedEnemy = Physics2D.OverlapBoxAll(player.transform.position + new Vector3(attackHitBox.offset.x  * player.facingDirection, attackHitBox.offset.y, 0) , attackHitBox.size, 0,playerData.whatIsEnemy);
        
        foreach (Collider2D coll in detectedEnemy)
        {
            coll.SendMessage("TakeDame", new AttackDetails(player.transform.position, DataGlobe.instance.playerAttributeData.curAttackDamage));
        }
    }
    public void CheckCanBounce()
    {
        if (isGrounded)
        {
            canBounce = false;
        }
        else if (detectedEnemy.Length != 0)
        {
            canBounce = true;
        }
        else if (inputY == 0)
        {
            canBounce = player.CheckIfTouchingBounceObject(new Vector2(player.facingDirection, 0));
        }
        else
        {
            canBounce = player.CheckIfTouchingBounceObject(new Vector2(0, inputY));
        }
    }
}
