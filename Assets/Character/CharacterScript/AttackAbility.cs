using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : CharacterAbility
{
    [SerializeField] private float attackDame;
    [SerializeField] private float cooldownTime;
    private float curCooldown;
    [Header("References")]
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private WallSlideAbility wallSlideAbility;
    [Header("Stats")]
    [SerializeField] private bool isAttack;

    protected override void Action()
    {
        if (curCooldown < 0)
        {

            if (Input.GetKey(KeyCode.W))
            {
                ani.SetTrigger("attackUp");
            }
            else if (!jumpAbility.isGround && Input.GetKey(KeyCode.S))
            {
                ani.SetTrigger("attackDown");
            }
            else if (wallSlideAbility.isWallSlide)
            {
                ani.SetTrigger("attackWallSlide");
            }
            else
            {
                ani.SetTrigger("attack");
            }

            curCooldown = cooldownTime;
        }
    }
    protected override void Update()
    {
        base.Update();
        if (curCooldown >= 0)
        {
            curCooldown -= Time.deltaTime;
        }
    }
    
}
