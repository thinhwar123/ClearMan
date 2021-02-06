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
    [SerializeField] public IEnumerator dashIenumrator;
    [SerializeField] public bool isDash;
    private float normalGravity;
    protected override void Awake()
    {
        base.Awake();
        normalGravity = rb.gravityScale;

    }
    protected override void Update()
    {
        base.Update();
        if (curCooldown >= 0)
        {
            curCooldown -= Time.deltaTime;
        }
        if (!isUnlock && isDash)
        {
            StopDash();
        }
    }
    protected override void Action()
    {
        if (curCooldown < 0)
        {
            curCooldown = cooldownTime;
            dashIenumrator = Dash();
            StartCoroutine(dashIenumrator);
        }           
    }
    public void StopDash()
    {
        StopCoroutine(dashIenumrator);
        rb.gravityScale = normalGravity;
        rb.velocity = Vector2.zero;
        isDash = false;
        ani.SetBool("isDash", false);
        characterMovement.isUnlock = true;
        UnlockAbility();
    }
    IEnumerator Dash()
    {
        LockAbility();
        isDash = true;
        ani.SetBool("isDash", true);
        characterMovement.isUnlock = false;
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;

        rb.AddForce(new Vector2(transform.localScale.x * dashForce, 0),ForceMode2D.Impulse);

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = normalGravity;
        isDash = false;
        ani.SetBool("isDash", false);
        characterMovement.isUnlock = true;
        UnlockAbility();
    }
}
