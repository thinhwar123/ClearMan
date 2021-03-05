using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class OnewayWall : MonoBehaviour
{
    [SerializeField] private List<GameObject> wallList;
    [SerializeField] private float curHitPoint;
    [SerializeField] private ParticleSystem effect;
    public void TakeDame(AttackDetails attackDetails)
    {
        if (curHitPoint > 0)
        {
            curHitPoint--;
            effect.Play();
            if (curHitPoint == 0)
            {

                GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
                Destroy(gameObject, 0.6f);
                foreach (GameObject wallObject in wallList)
                {
                    wallObject.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
                    Destroy(wallObject, 0.6f);
                }
            }
        }

    }
}
