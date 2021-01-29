using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunStatus : CharacterStatus
{
    protected override IEnumerator Action(float duration)
    {
        LockAbility();
        // them ani stun
        yield return new WaitForSeconds(duration);
        UnlockAbility();
    }
}
