using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DisapearPlatform : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float timeApear;
    [SerializeField] private LayerMask whatIsPlayer;
    [SerializeField] private BoxCollider2D checkCollider;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private bool active;
    void Start()
    {
        active = true;
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (active && CheckIfTouchingPlayer())
        {
            StartCoroutine(StartDisapear());
        }
    }
    public bool CheckIfTouchingPlayer()
    {
        return Physics2D.OverlapBox(checkCollider.transform.position, checkCollider.size, 0, whatIsPlayer);
    }
    IEnumerator StartDisapear()
    {
        active = false;
        yield return new WaitForSeconds(duration);
        coll.isTrigger = true;
        sprite.DOFade(0, 0.5f);
        yield return new WaitForSeconds(timeApear);
        coll.isTrigger = false;
        sprite.DOFade(1, 0.5f);
        active = true;
    }
}
