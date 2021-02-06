using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleJumpAbility : CharacterAbility
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float timeJump;
    private float curTime;
    [SerializeField] private LayerMask whatIsGround;
    [Header("References")]
    [SerializeField] private GameObject foot;
    [SerializeField] private CharacterEffect characterEffect;
    [SerializeField] private DoubleJumpAbility doubleJumpAbility;
    [SerializeField] private SwimAbility swimAbility;
    [Header("Stats")]
    [SerializeField] private bool isJump;
    [SerializeField] public bool canJump;
    [SerializeField] public bool isGround;
    protected override void Action()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    protected override void Update()
    {
        if (Input.GetKeyDown(keyActive) && isUnlock && canJump && !isGround && !doubleJumpAbility.canJump && !doubleJumpAbility.isJump && !swimAbility.isWater)
        {
            isJump = true;
            canJump = false;
            curTime = timeJump;
            ani.SetFloat("jumpType", -1);
            characterEffect.JumpEffect();
            Action();
        }
        if (Input.GetKey(keyActive) && isJump )
        {
            if (curTime >= 0 && isUnlock)
            {
                Action();
                curTime -= Time.deltaTime;
            }
            else
            {
                isJump = false;
            }

        }
        if (Input.GetKeyUp(keyActive))
        {
            isJump = false;
        }

        isGround = Physics2D.OverlapBox(foot.transform.position, foot.GetComponent<BoxCollider2D>().size, 0, whatIsGround);
        if (isGround || swimAbility.isWater)
        {
            canJump = true;
        }

    }
}
