using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/EntityData/Base Data")]
public class D_Entity : ScriptableObject
{
    public float maxHealth = 30;
    public float hitDame = 1;

    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.2f;

    public float maxAgroDistance = 4;
    public float minAgroDistance = 3;

    public float closeRangeAction = 1;

    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public LayerMask whatIsPlayer;
}
