using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstAbility : CharacterAbility
{
    [SerializeField] private float range;
    [SerializeField] private LayerMask whatIsWeakObject;
    [SerializeField] private float delayTime;
    [Header("References")]
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private CharacterEffect characterEffect;
    [Header("Stats")]
    [SerializeField] private bool isBurst;
    protected override void Action()
    {
        if (jumpAbility.isGround && isBurst)
        {
            isBurst = false;
            RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, range, Vector2.zero, 0,whatIsWeakObject);
            if (hit != null)
            {
                for (int i = 0; i < hit.Length; i++)
                {
                    hit[i].collider.GetComponent<WeakObject>().DestroyObject();
                }
            }
            ani.SetTrigger("burst");
            StartCoroutine( Delay());
            

            characterEffect.ExplosionEffect();
        }
    }
    IEnumerator Delay()
    {
        characterMovement.isUnlock = false;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        characterMovement.isUnlock = true;
        yield return new WaitForSeconds(delayTime - 0.5f);
        isBurst = true;
    }
}
