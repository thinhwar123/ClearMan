using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WallJumpAbility : CharacterAbility
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    private float normalGravity;
    [Header("References")]
    [SerializeField] private CharacterEffect characterEffect;
    [SerializeField] private WallSlideAbility wallSlideAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] public bool isWallJump;
    protected override void Awake()
    {
        base.Awake();
        normalGravity = rb.gravityScale;
    }
    protected override void Action()
    {
        if (wallSlideAbility.isWallSlide)
        {
            if (characterMovement.directionMovement.x * transform.localScale.x > 0)
            {
                StartCoroutine(Jump(0));
            }
            else 
            {
                StartCoroutine(Jump(1));
            }

        }
    }
    public IEnumerator Jump(int type)
    {
        isWallJump = true;
        LockAbility();
        characterMovement.isUnlock = false;
        rb.gravityScale = normalGravity;
        ani.SetBool("isWallSlide", false);
        ani.SetFloat("jumpType", 1);
        characterEffect.JumpEffect();
        rb.velocity = new Vector2(-transform.localScale.x, 1) * jumpForce;

        yield return new WaitForSeconds(jumpTime );
        yield return new WaitForSeconds(jumpTime * type);
        isWallJump = false;
        UnlockAbility();
        characterMovement.isUnlock = true;

    }
}
