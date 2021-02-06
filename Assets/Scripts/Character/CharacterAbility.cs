using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    protected Animator ani;
    protected Rigidbody2D rb;
    [Header("Config")]
    [SerializeField] protected List<CharacterAbility> listLockAbility;
    [SerializeField] public bool isUnlock;
    [SerializeField] protected KeyCode keyActive;
    protected virtual void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        PlayerPrefs.SetInt(this.GetType().Name, isUnlock ? 1 : 0);
    }
    protected virtual void Update()
    {
        if (Input.GetKeyDown(keyActive) && isUnlock)
        {
            Action();
        }
    }
    protected virtual void Action()
    {

    }
    protected virtual void LockAbility()
    {
        for (int i = 0; i < listLockAbility.Count; i++)
        {
            listLockAbility[i].isUnlock = false;
        }
    }
    protected virtual void UnlockAbility()
    {
        for (int i = 0; i < listLockAbility.Count; i++)
        {
            listLockAbility[i].isUnlock = (PlayerPrefs.GetInt(listLockAbility[i].GetType().Name) == 1 ? true : false);
            
        }
    }
}
