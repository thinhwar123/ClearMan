using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveAbility : CharacterAbility
{
    [SerializeField] private float timeDelaySave;
    [Header("References")]
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private bool canSave;
    [SerializeField] private bool isSave;
    protected override void Action()
    {
        StartCoroutine(Save());
    }
    IEnumerator Save()
    {
        isUnlock = false;
        if (isSave)
        {
            UnlockAbility();
            characterMovement.isUnlock = true;
        }
        else
        {
            rb.velocity = Vector2.zero;
            LockAbility();
            characterMovement.isUnlock = false;
            SaveData();
        }
        ani.SetTrigger("save");
        yield return new WaitForSeconds(timeDelaySave);
        isUnlock = true;

    }
    public void SaveData()                    // luu du lieu game
    {

    }
}
