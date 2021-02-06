using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToStateMachine : MonoBehaviour
{
    public AttackState attackState;
    public MoveState moveState;
    public void TriggerAttack()
    {
        attackState.TriggerAttack();
    }
    public void FinishAttack()
    {
        attackState.FinishAttack();
    }
    public void TriggerMove()
    {
        moveState.TriggerMove();
    }
    public void FinishMove()
    {
        moveState.FinishMove();
    }
}
