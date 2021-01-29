using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class WeakObject : MonoBehaviour
{
    public void DestroyObject()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 2);
        //ani
        Destroy(gameObject, 2);
    }
}
