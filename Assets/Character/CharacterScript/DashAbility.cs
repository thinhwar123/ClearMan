using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : CharacterAbility
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashTime;
    [SerializeField] private float cooldownTime;
    private float curCooldown;
    [Header("References")]
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] public bool isDash;
    protected override void Update()
    {
        base.Update();
        if (curCooldown >= 0)
        {
            curCooldown -= Time.deltaTime;
        }
    }
    protected override void Action()
    {
        if (curCooldown < 0)
        {
            curCooldown = cooldownTime;
            StartCoroutine(Dash());
        }           
    }
    IEnumerator Dash()
    {
        LockAbility();
        isDash = true;
        ani.SetBool("isDash", true);
        characterMovement.isUnlock = false;
        float curGravity = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
        //Debug.Log(new Vector2(transform.localScale.x * dashForce, 0));
        rb.AddForce(new Vector2(transform.localScale.x * dashForce, 0),ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = curGravity;
        isDash = false;
        ani.SetBool("isDash", false);
        characterMovement.isUnlock = true;
        UnlockAbility();

    }
}
