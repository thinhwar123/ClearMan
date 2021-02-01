using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newEntityData", menuName = "Data/EntityData/Base Data")]
public class D_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.2f;
    public float ledgeCheckDistance = 0.4f;
    public float maxAgroDistance = 4;
    public float minAgroDistance = 3;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public LayerMask whatIsPlayer;
}
