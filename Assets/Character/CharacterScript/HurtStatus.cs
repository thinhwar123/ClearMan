using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HurtStatus : CharacterStatus
{

    [SerializeField] private float timeStop;
    [SerializeField] private float timeScale;
    [SerializeField] private float attackForce;
    [Header("References")]
    [SerializeField] private CharacterAtrribute characterAtrribute;
    [Header("Status")]
    [SerializeField] private bool isHurt;
    public void Trigger(float duration, Vector2 direction)
    {
        StartCoroutine(Action(duration, direction));
    }
    protected IEnumerator Action(float duration, Vector2 direction)
    {
        LockAbility();
        isHurt = true;
        ani.SetTrigger("hurt");
        transform.DOMove((Vector2)transform.position + direction * attackForce, timeStop * timeScale);
        characterAtrribute.isUnavailable = true;
        GetComponent<CharacterMovement>().isUnlock = false;
        Time.timeScale = timeScale;
        ani.GetComponent<SpriteRenderer>().DOColor(new Color(0.5f, 0.5f, 0.5f), timeStop * timeScale /6).SetLoops(6, LoopType.Yoyo);
        yield return new WaitForSecondsRealtime(timeStop);
        UnlockAbility();
        GetComponent<CharacterMovement>().isUnlock = true;
        Time.timeScale = 1.0f;
        ani.GetComponent<SpriteRenderer>().DOColor(new Color(0.5f, 0.5f, 0.5f), (duration - timeStop) / 6).SetLoops(6, LoopType.Yoyo);
        yield return new WaitForSecondsRealtime((duration - timeStop));
        characterAtrribute.isUnavailable = false;
        isHurt = false;
    }
}


