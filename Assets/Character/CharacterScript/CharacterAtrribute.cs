using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CharacterAtrribute : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public int maxHitPoint;
    [SerializeField] public int maxManaPoint;
    [SerializeField] public int curHitPoint;
    [SerializeField] public int curManaPoint;
    [SerializeField] public bool isUnavailable;
    private bool isWaitting;
    void Awake()
    {
        curHitPoint = maxHitPoint;
        curManaPoint = maxManaPoint;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDame(1 , Vector2.left);
            //ani.GetComponent<SpriteRenderer>().DOColor(new Color(0.8f,0.8f,0.8f), timeStop).SetLoops(10, LoopType.Yoyo);
        }
    }
    public void TakeDame(int damage, Vector2 dir)
    {
        if (isUnavailable)
        {
            return;
        }
        curHitPoint -= damage;
        GetComponent<HurtStatus>().Trigger(2, dir);
        if (curHitPoint <=0 )
        {
            curHitPoint = 0;
            Death();
        }

    }
    public void Heal(int hitPoint)
    {
        curHitPoint += hitPoint;
        if (curHitPoint > maxHitPoint)
        {
            curHitPoint = maxHitPoint;
        }
    }
    public void Death()
    {
        GetComponent<DeathStatus>().Trigger(2);
    }
    public bool UseMana(int mana)
    {
        if (curManaPoint >= mana)
        {
            curManaPoint -= mana;
            return true;
        }
        return false;
    }
    public void RegenMana()
    {

    }
    public bool IsFullHitPoint()
    {
        return curHitPoint == maxHitPoint;
    }
}
