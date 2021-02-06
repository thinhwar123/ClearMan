using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DashEffect : MonoBehaviour
{
    public void Action(Sprite sprite, float time, Vector3 localScale)
    {
        transform.localScale = localScale;
        GetComponent<SpriteRenderer>().sprite = sprite;
        GetComponent<SpriteRenderer>().DOFade(0, time);
        Destroy(gameObject, time);
    }
}
