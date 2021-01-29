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
        if ( isUnlock && swimAbility.isSwim)
        {
            if (!body.isTrigger)
            {
                if (characterMovement.directionMovement.y < 0)
                {
                    body.isTrigger = true;
                    rb.gravityScale = 0;
                    isBubble = true;
                    ani.SetBool("isBubble", true) ;
                }
            }
            else
            {
                Vector2 direction = new Vector2(rb.velocity.x, upForce + characterMovement.directionMovement.y * downForce);
                rb.velocity = direction;
                if (isBubble && characterMovement.directionMovement.y >=0)
                {
                    isInWater = Physics2D.OverlapBox(body.transform.position, body.size,0, whatIsWater);
                    if (!isInWater)
                    {
                        body.isTrigger = false;
                        rb.gravityScale = normalGravity;
                        isBubble = false;
                        ani.SetBool("isBubble", false);
                    }
                }
                
            }
        }
    }
}
