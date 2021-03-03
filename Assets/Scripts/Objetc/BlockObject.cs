using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    public Collider2D coll1;
    public Collider2D coll2;
    private void Awake()
    {
        Physics2D.IgnoreCollision(coll1, coll2);
    }
}
