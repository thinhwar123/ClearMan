using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunStatus : CharacterStatus
{
    
    protected override IEnumerator Action(float duration)
    {
        LockAbility();
        GetComponent<CharacterMovement>().isUnlock = false;
        rb.velocity = Vector2.zero;
        ani.SetBool("isStun", true);
        yield return new WaitForSeconds(duration);
        UnlockAbility();
        GetComponent<CharacterMovement>().isUnlock = true;
        ani.SetBool("isStun", false);
    }
}
