using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAbility : CharacterAbility
{
    [SerializeField] private float upForce;
    [SerializeField] private float downForce;
    [SerializeField] private LayerMask whatIsWater;
    [Header("References")]
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private HurtStatus hurtStatus;
    [SerializeField] private SwimAbility swimAbility;
    [SerializeField] private BoxCollider2D body;
    [Header("Stats")]
    [SerializeField] public bool isBubble;
    [SerializeField] public bool isInWater;
    private float normalGravity;
    protected override void Awake()
    {
        base.Awake();
        normalGravity = rb.gravityScale;
    }
    protected override void Update()
    {
        if (swimAbility.isSwim  && isUnlock)
        {
            if (!body.isTrigger)
            {
                if (characterMovement.directionMovement.y < 0)
                {
                    body.isTrigger = true;
                    rb.gravityScale = 0;
                    isBubble = true;
                    LockAbility();
                    ani.SetBool("isBubble", true) ;
                }
            }
            else
            {
                if (isUnlock)
                {
                    Vector2 direction = new Vector2(rb.velocity.x, upForce + characterMovement.directionMovement.y * downForce);
                    rb.velocity = direction;
                }

                if (isBubble && characterMovement.directionMovement.y >= 0)
                {
                    isInWater = Physics2D.OverlapBox(body.transform.position, body.size,0, whatIsWater);
                    if (!isInWater)
                    {
                        body.isTrigger = false;
                        rb.gravityScale = normalGravity;
                        isBubble = false;
                        UnlockAbility();
                        ani.SetBool("isBubble", false);
                    }
                }
                
            }
        }
        if (hurtStatus.isHurt && isBubble)
        {
            isInWater = Physics2D.OverlapBox(body.transform.position, body.size, 0, whatIsWater);
            if (!isInWater)
            {
                body.isTrigger = false;
                rb.gravityScale = normalGravity;
                isBubble = false;
                UnlockAbility();
                ani.SetBool("isBubble", false);
            }
            else
            {

            }
        }
    }
}
