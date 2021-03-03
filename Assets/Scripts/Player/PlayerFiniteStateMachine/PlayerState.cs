using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected PlayerAbilityData playerData;

    protected bool isAnimationFinish;
    public bool isExitingState;

    protected float startTime;

    private string animBoolName;
    public PlayerState(Player player, PlayerStateMachine stateMachine, PlayerAbilityData playerData, string animBoolName)
    {
        this.player = player;
        this.stateMachine = stateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        //Debug.Log(animBoolName);
        DoChecks();
        startTime = Time.time;
        player.anim.SetBool(animBoolName, true);

        isAnimationFinish = false;
        isExitingState = false;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        isExitingState = true;
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {
        DoChecks();
    }
    public virtual void DoChecks()
    {

    }
    public virtual void AnimationTrigger()
    {

    }
    public virtual void AnimationFinish()
    {
        isAnimationFinish = true;
    }
}
