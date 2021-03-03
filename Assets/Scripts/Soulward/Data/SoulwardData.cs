using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[CreateAssetMenu(fileName = "newSoulwardData", menuName = "Data/Soulward Data/Base Data")]
public class SoulwardData : ScriptableObject
{

    [Header("FollowConfig")]
    public float appearTime = 0.1f;
    public float disappearTime = 0.1f; 
    public float movementSpeed;
    public float distance;
    public Ease easeX;
    public Ease easeY;

    [Header("ChangeTypeConfig")]
    public List<Sprite> spriteList;
    public List<Color> colorList;

    [Header("LightConfig")]
    public float intensity = 1;

    [Header("TeleportConfig")]
    public float distanceAimTeleport = 1;
    public float shootVelocity = 10;
    public float timeTravel = 3f;
    public LayerMask whatIsGround;
}
