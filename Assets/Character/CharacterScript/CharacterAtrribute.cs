﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CharacterAtrribute : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] public float maxHitPoint;
    [SerializeField] public float maxManaPoint;
    [SerializeField] public float curHitPoint;
    [SerializeField] public float curManaPoint;
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
            //TakeDame(1 , Vector2.left);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            GetComponent<StunStatus>().Trigger(2);
        }
    }
    public void TakeDame(AttackDetails attackDetails)
    {
        if (!isUnavailable)
        {
            curHitPoint -= attackDetails.attackDamage;
            GetComponent<HurtStatus>().Trigger(attackDetails.position);
            if (curHitPoint <= 0)
            {
                curHitPoint = 0;
                Death();
            }
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
