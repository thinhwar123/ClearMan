using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideAbility : CharacterAbility
{
    [SerializeField] private float slideForce;
    [SerializeField] private LayerMask whatIsWall;
    private float normalGravity;
    [Header("References")]
    [SerializeField] private GameObject hand;
    [SerializeField] private CharacterEffect characterEffect;
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private bool isWall;
    [SerializeField] public bool isWallSlide;
    private bool lastCheck;
    protected override void Awake()
    {
        base.Awake();
        normalGravity = rb.gravityScale;
    }
    protected override void Update()
    {
        if (isUnlock)
        {
            Action();
            
        }
    }
    protected override void Action()
    {
        isWall = Physics2D.OverlapBox(hand.transform.position, hand.GetComponent<BoxCollider2D>().size, 0, whatIsWall);
        if (isWall && !jumpAbility.isGround && rb.velocity.y < 0 && (characterMovement.directionMovement.x * transform.localScale.x > 0 || isWallSlide))
        {
            LockAbility();
            rb.gravityScale = 0;
            rb.velocity = Vector2.down * slideForce;
            isWallSlide = true;
            ani.SetBool("isWallSlide", true);
            //characterEffect.WallSlide(true);
        }
        else if( lastCheck)
        {
            UnlockAbility();
            rb.gravityScale = normalGravity;
            isWallSlide = false;
            ani.SetBool("isWallSlide", false);
            //characterEffect.WallSlide(false);
        }
        lastCheck = isWallSlide;
    }

}
