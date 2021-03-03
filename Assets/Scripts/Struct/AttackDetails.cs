using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct AttackDetails 
{
    public Vector2 position;
    public float attackDamage;

    public AttackDetails(Vector2 position, float attackDamage)
    {
        this.position = position;
        this.attackDamage = attackDamage;
    }
}
