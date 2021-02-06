using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMeleeAttackStateData", menuName = "Data/State/Melee Attack State")]
public class D_MeleeAttackState : ScriptableObject
{
    public Vector2 attackDirection = Vector2.zero; //huong di chuyen luc tan cong
    public float speedWhileAttacl = 0; //toc do di chuyen luc dang tan cong
    public float attackRadius = 0.5f;
    public float attackDamage = 1;
    public LayerMask whatIsPlayer;
}
