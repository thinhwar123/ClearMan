using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HurtStatus : CharacterStatus
{
    [SerializeField] private float duration;
    [SerializeField] private float timeStop;
    [SerializeField] private float timeScale;
    [SerializeField] private float attackForce;
    [Header("References")]
    [SerializeField] private CharacterAtrribute characterAtrribute;
    [SerializeField] private CharacterEffect characterEffect;
    [Header("Status")]
    [SerializeField] private bool isHurt;
    public void Trigger(Vector2 position)
    {
        StartCoroutine(Action(position));
    }
    protected IEnumerator Action(Vector2 position)
    {
        LockAbility();
        isHurt = true;
        ani.SetTrigger("hurt");
        characterEffect.HurtEffect();
        characterAtrribute.isUnavailable = true;
        GetComponent<CharacterMovement>().isUnlock = false;
        Time.timeScale = timeScale;

        yield return new WaitForSecondsRealtime(timeStop);

        Time.timeScale = 1.0f;
        ani.GetComponent<SpriteRenderer>().DOColor(new Color(0.5f, 0.5f, 0.5f), duration / 6).SetLoops(6, LoopType.Yoyo);
        if (transform.position.x > position.x)
        {
            rb.velocity = new Vector2(1, 1) * attackForce;
        }
        else
        {
            rb.velocity = new Vector2(-1, 1) * attackForce;
        }


        yield return new WaitForSecondsRealtime(timeStop);

        UnlockAbility();
        GetComponent<CharacterMovement>().isUnlock = true;

        yield return new WaitForSecondsRealtime(duration - timeStop);
        characterAtrribute.isUnavailable = false;
        isHurt = false;
    }
}


