using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    public BoxCollider2D startPoint;
    public BoxCollider2D endPoint;
    public LayerMask whatIsPlayer;
    public RaycastHit2D CheckTouchingPlayer()
    {
        return Physics2D.BoxCast(startPoint.transform.position, startPoint.size, 0, Vector2.zero, 0, whatIsPlayer);
    }

    public void Update()
    {
        RaycastHit2D hit = CheckTouchingPlayer();
        if (hit)
        {
            hit.transform.position = endPoint.transform.position;
        }
    }
}
