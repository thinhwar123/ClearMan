using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToCharacter : MonoBehaviour
{
    public AttackAbility attackAbility;
    public void TriggerAttack()
    {
        attackAbility.TriggerAttack();
    }
    public void TriggerAttackUp()
    {
        attackAbility.TriggerAttackUp();
    }
    public void TriggerAttackDown()
    {
        attackAbility.TriggerAttackDown();
    }
    public void TriggerAttackWallSlide()
    {
        attackAbility.TriggerAttackWallSlide();
    }
}
