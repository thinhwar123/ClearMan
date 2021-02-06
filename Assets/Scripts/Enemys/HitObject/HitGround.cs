using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitGround : MonoBehaviour
{
    [SerializeField] private Entity entity;
    void Start()
    {
       
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") && !entity.isDead)
        {
            entity.OnHitGround(collision);
        }

    }
}
