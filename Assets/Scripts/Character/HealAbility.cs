using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAbility : CharacterAbility
{
    [SerializeField] private float timeHold;
    [SerializeField] private int manaUse;
    [SerializeField] private int hitpoint;
    private float curTime;
    [Header("References")]
    [SerializeField] private CharacterEffect characterEffect;
    [SerializeField] private CharacterAtrribute characterAtrribute;
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private SwimAbility swimAbility;
    [SerializeField] private WallSlideAbility wallSlideAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private bool isHeal;

    protected override void Awake()
    {
        base.Awake();
        curTime = timeHold;
    }
    protected override void Action()
    {

    }
    protected override void Update()
    {
        if (Input.GetKey(keyActive) && isUnlock && jumpAbility.isGround && !swimAbility.isWater && !wallSlideAbility.isWallSlide )
        {
            LockAbility();
            characterMovement.isUnlock = false;
            isHeal = true;
            ani.SetBool("isHeal", true);
            if (curTime < 0)
            {
                if (!characterAtrribute.IsFullHitPoint() && characterAtrribute.UseMana(manaUse))
                {
                    characterEffect.HealEffect();
                    curTime = timeHold;
                    //heal
                    characterAtrribute.Heal(hitpoint);
                }
            }
            else
            {
                curTime -= Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(keyActive) )
        {
            UnlockAbility();
            characterMovement.isUnlock = true;
            isHeal = false;
            ani.SetBool("isHeal", false);
            curTime = timeHold;
        }
    }
}
