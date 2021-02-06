using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour
{
    protected Animator ani;
    protected Rigidbody2D rb;
    [Header("Config")]
    [SerializeField] protected List<CharacterAbility> listLockAbility;
    protected virtual void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Trigger(float duration)
    {
        StartCoroutine(Action(duration));
    }
    protected virtual IEnumerator Action(float duration)
    {
        yield return new WaitForSeconds(duration);

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
