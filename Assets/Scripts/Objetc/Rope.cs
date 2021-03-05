using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Rope : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rbRock;
    private bool isCut;
    private void Start()
    {
        isCut = false;
    }
    public void TakeDame(AttackDetails attackDetails)
    {
        if (!isCut)
        {
            isCut = true;
            GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
            Destroy(gameObject, 0.6f);
            rbRock.transform.parent = transform.parent;
            rbRock.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
