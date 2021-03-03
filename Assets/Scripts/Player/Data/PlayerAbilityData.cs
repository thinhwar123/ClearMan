using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Player Ability Data")]
public class PlayerAbilityData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;

    [Header("Jump State")]
    public float jumpVelocity = 10f;
    public float timeJumpHold = 1f;
    public int amountOfJump = 1;
    
    [Header("In Air State")]
    public float coyoteTime = 0.2f;
    public float wallJumpCoyoteTime = 0.2f;
    public float variableJumpHeightMultiplier = 0.5f;

    [Header("Wall Slide State")]
    public float wallSlideVelocity = 3f;

    [Header("Wall Jump State")]
    public float wallJumpVelocity = 5f;
    public float wallJumpTime = 1f;
    public Vector2 wallJumpAngle = new Vector2(1, 2);

    [Header("Dash State")]
    public float dashVelocity = 10f;
    public float dashTime = 1f;
    public float delayBetweenDash = 0.2f;

    [Header("Change Soulward State")]
    public int soulwardType;
    public float changeSoulwardTimeScale = 0;
    public float safeUIDistance = 1;

    [Header("Burst State")]
    public float burstRange = 2f;
    public LayerMask whatIsWeakObject;
    public float burstDelayTime = 1f;

    [Header("Wing Jump State")]
    public float wingJumpVelocity = 10f;

    [Header("Glide State")]
    public float glideVelocity = 10f;

    [Header("Teleport State")]
    public float teleportTimeScale = 0.1f;
    public float aimTimeTeleport = 1f;
    public float delayTimeTeleport = 1f;

    [Header("Arrow State")]
    public float arrowTimeScale = 0.1f;
    public float aimTimeArrow = 1f;
    public float delayTimeArrow = 1f;

    [Header("Swim State")]
    public float swimVelocity = 3;

    [Header("Bubble State")]
    public float upForce = 1;
    public float downForce = 2;
    public float bubbleVelocityX = 1;

    [Header("Move Object State")]
    public float moveObjectVelocity = 3;

    [Header("Look State")]
    public float timeHoldInput = 1f;

    [Header("Bounce State")]
    public float bounceForce = 1f;
    public float bounceTime = 1f;

    [Header("Hurt State")]
    public float hurtTimeScale = 1f;
    public Vector2 angle = new Vector2(1, 1);
    public float pushBackForce = 1f;
    public float unavailableTime = 1f;

    [Header("Heal State")]
    public float timeBetweenHeal = 1f;
    public float healValue = 1f;
    public float manaUseHeal = 1f;

    [Header("Stun State")]
    public float stunTime = 1f;

    [Header("Check Variable")]
    public float groundCheckRadius = 10f;
    public float wallCheckDistance = 1f;
    public float windCheckRadius = 10f;
    public float waterCheckRadius = 10f;
    public float waterDeepCheckRadius = 10f;
    public float physicalObjectCheckDistance = 1f;
    public float bounceObjectCheckDistance = 1f;
    public LayerMask whatIsGround;
    public LayerMask whatIsWind;
    public LayerMask whatIsWater;
    public LayerMask whatIsPhysicalObject;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsBounceObject;
    public LayerMask whatIsInteractionItem;
}
