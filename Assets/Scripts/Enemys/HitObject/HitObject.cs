using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObject : MonoBehaviour
{
    public D_HitObject hitData;
    private Entity entity;
    public void Start()
    {
        entity = transform.parent.parent.GetComponent<Entity>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && !entity.isDead)
        {
            hitData.attackDetails.position = transform.position;
            collision.transform.SendMessage("TakeDame", hitData.attackDetails);
            entity.OnHitPlayer();
        }
    }
    public void Deactive()
    {
        GetComponent<CapsuleCollider2D>().isTrigger = true;
    }
}
