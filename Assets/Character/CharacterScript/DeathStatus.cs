using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathStatus : CharacterStatus
{
    protected override IEnumerator Action(float duration)
    {
        ani.SetTrigger("death");
        //LockAbility();
        yield return new WaitForSeconds(duration);
    }
}
