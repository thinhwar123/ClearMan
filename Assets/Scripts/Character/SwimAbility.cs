using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimAbility : CharacterAbility
{
    [SerializeField] private LayerMask whatIsWater;
    [Header("References")]
    [SerializeField] private GameObject foot;
    [Header("Stats")]
    [SerializeField] public bool isSwim;
    [SerializeField] public bool isWater;
    private bool lastCheck;
    protected override void Update()
    {
        isWater = Physics2D.OverlapBox(foot.transform.position, foot.GetComponent<BoxCollider2D>().size, 0, whatIsWater);
        Action();
    }
    protected override void Action()
    {
        
        if (isWater && !lastCheck)
        {
            LockAbility();
            isSwim = true;
            ani.SetBool("isWater", true);
        }
        else if(!isWater && lastCheck)
        {
            UnlockAbility();
            ani.SetBool("isWater", false);
            isSwim = false;
        }
        lastCheck = isWater;
    }
}
