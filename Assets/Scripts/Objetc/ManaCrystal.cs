using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaCrystal : MonoBehaviour
{
    private int curHitPoint;
    private SpriteRenderer sprite;
    [SerializeField] private List<Sprite> spriteList;
    [SerializeField] private ParticleSystem effect;
    public void Start()
    {
        curHitPoint = 3;
        sprite = GetComponent<SpriteRenderer>();
    }
    public void TakeDame(AttackDetails attackDetails)
    {
        if (curHitPoint > 0)
        {
            effect.Play();
            DataGlobe.instance.player.GetComponent<Player>().GainMana(1);
            curHitPoint--;
            sprite.sprite = spriteList[curHitPoint];
        }
    }
}
