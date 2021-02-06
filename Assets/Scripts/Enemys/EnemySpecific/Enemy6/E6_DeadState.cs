﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E6_DeadState : DeadState
{
    private Enemy6 enemy;
    public E6_DeadState(Entity entity, FinteStateMachine stateMachine, string animBoolName, D_DeadState stateData, Enemy6 enemy) : base(entity, stateMachine, animBoolName, stateData)
    {
        this.enemy = enemy;
    }

    public override void DoCheck()
    {
        base.DoCheck();
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
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
