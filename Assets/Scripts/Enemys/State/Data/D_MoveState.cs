﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newMoveStateData", menuName = "Data/State/Move State")]
public class D_MoveState : ScriptableObject
{
    public float movementSpeed = 3;
    public float jumpForce = 3;
}
