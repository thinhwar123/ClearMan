using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject : MonoBehaviour
{
    [SerializeField] Collider2D physicalObject;
    [SerializeField] Collider2D blockObject;
    public void Start()
    {
        Physics2D.IgnoreCollision(physicalObject, blockObject);
    }
}
