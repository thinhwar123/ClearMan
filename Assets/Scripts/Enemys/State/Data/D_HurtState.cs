using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newHurtStateData", menuName = "Data/State/Hurt State")]
public class D_HurtState : ScriptableObject
{
    public float knockBackSpeed = 1;
    public float duration = 1;
}
