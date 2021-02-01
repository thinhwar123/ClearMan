﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newIdleStateData", menuName = "Data/State/Idle State")]
public class D_IdleState : ScriptableObject
{
    public float minIdleTime = 1;
    public float maxIdleTime = 2;

}
