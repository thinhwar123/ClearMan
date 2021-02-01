﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State 
{
    protected FinteStateMachine stateMachine;
    protected Entity entity;
    protected float startTime;
    protected string animBoolName;

    public State(Entity entity, FinteStateMachine stateMachine, string animBoolName)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        startTime = Time.time;
        entity.ani.SetBool(animBoolName, true);
        DoCheck();
    }
    public virtual void Exit()
    {
        entity.ani.SetBool(animBoolName, false);
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicUpdate()
    {
        DoCheck();
    }
    public virtual void DoCheck()
    {

    }
}
