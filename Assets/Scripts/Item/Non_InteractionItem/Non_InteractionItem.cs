using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Non_InteractionItem : MonoBehaviour
{
    public bool isPickUp { get; private set; }
    public bool canPickUp { get; private set; }
    public ItemData baseData;
    public int count;
    public LayerMask whatIsPlayer;

    private CircleCollider2D coll;
    private SpriteRenderer sprite;

    public void Awake()
    {
        canPickUp = true;
        isPickUp = true;
    }
    private void Start()
    {
        //isPickUp = false;
        coll = GetComponent<CircleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (CheckIfTouchingPlayer())
        {
            isPickUp = true;
            DataGlobe.instance.PickItem(baseData, count);
            sprite.DOFade(0, 0.2f);
            Destroy(gameObject, 0.3f);
        }
    }
    public bool CheckIfTouchingPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, coll.radius, whatIsPlayer) && !isPickUp;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && canPickUp)
        {
            isPickUp = false;
            canPickUp = false;
        }
    }
}
