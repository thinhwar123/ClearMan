using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenBookAbility : CharacterAbility
{
    [SerializeField] private float timeOpenBook;
    [Header("References")]
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private SwimAbility swimAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private bool isOpenBook;
    protected override void Action()
    {
        if (jumpAbility.isGround && !swimAbility.isWater && rb.velocity == Vector2.zero)
        {
            StartCoroutine(OpenBook());
        }
    }
    IEnumerator OpenBook()
    {
        if (isOpenBook)
        {
            isOpenBook = false;
            ani.SetBool("isOpenBook", false);
            yield return new WaitForSeconds(timeOpenBook);
            UnlockAbility();
            characterMovement.isUnlock = true;
            // ui inventory
        }
        else
        {
            isOpenBook = true;
            LockAbility();
            characterMovement.isUnlock = false;
            ani.SetBool("isOpenBook", true);
            yield return new WaitForSeconds(timeOpenBook);
            // ui inventory
        }
    }
}
