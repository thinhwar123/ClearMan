using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newLookForPlayerStateData", menuName = "Data/State/Look For Player State")]
public class D_LookForPlayerState : ScriptableObject
{
    public int amountOfTurn = 2;
    public float timeBetweenTurn = 0.2f;
}
