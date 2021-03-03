using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBulletUIData", menuName = "Data/Object Data/Bullet Data")]
public class BulletData : ScriptableObject
{
    public float bulletSpeed = 10f;
    public int maxBounce;
}
