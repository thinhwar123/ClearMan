﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public FinteStateMachine stateMachine;
    public D_Entity entityData;
    public int facingDirection;
    public Rigidbody2D rb { get; private set; }
    public Animator ani { get; private set; }
    public GameObject aliveGO { get; private set; }

    public AnimationToStateMachine atsm { get; private set; }
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected Transform ledgeCheck;
    [SerializeField] protected Transform playerCheck;
    [SerializeField] protected HitObject hitObject;
    [SerializeField] protected float curHealth;
    [SerializeField] public bool isDead;
    [SerializeField] protected bool drawGizmos;
    private Vector2 velocityWorkspace;
    public virtual void Start()
    {
        facingDirection = -1;
        curHealth = entityData.maxHealth;
        aliveGO = transform.Find("AliveGO").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        ani = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
        isDead = false;
        stateMachine = new FinteStateMachine();
    }
    public virtual void Update()
    {
        stateMachine.currentState.LogicUpdate();
    }
    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicUpdate();
    }
    public virtual void TakeDame(AttackDetails attackDetails)
    {
        if (isDead)
        {
            return;
        }
        SetVelocity(0);
        curHealth -= attackDetails.attackDamage;
        if (curHealth <= 0)
        {
            isDead = true;
            hitObject.Deactive();
        }
    }
    public virtual void SetGravity(float gravityScale)
    {
        rb.gravityScale = gravityScale;
    }
    public virtual void SetVelocity(float velocity, Vector2 direction)
    {
        velocityWorkspace.Set(direction.x * velocity, direction.y);
        rb.velocity = velocityWorkspace;
    }
    public virtual void SetVelocity(float velocity, Vector2 direction, bool isGoAround)
    {
        if (isGoAround)
        {
            velocityWorkspace.Set(direction.x * velocity, direction.y * velocity);
            rb.velocity = velocityWorkspace;
        }

    }
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }
    public virtual void OnHitPlayer()
    {

    }
    public virtual void OnHitGround(Collision2D collision)
    {

    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, entityData.wallCheckDistance, entityData.whatIsWall);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.right * facingDirection, entityData.minAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.right * facingDirection, entityData.maxAgroDistance, entityData.whatIsPlayer);
    }
    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, Vector2.right * facingDirection, entityData.closeRangeAction, entityData.whatIsPlayer);
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }
    public virtual void Flip(int dir)
    {
        if (dir != facingDirection)
        {
            facingDirection = dir;
            aliveGO.transform.Rotate(0f, 180f, 0f);
        }

    }
    public virtual void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
            Gizmos.DrawLine(playerCheck.position + Vector3.up * 0.1f, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance) + Vector3.up * 0.1f);
            Gizmos.DrawLine(playerCheck.position + Vector3.down * 0.1f, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance) + Vector3.down * 0.1f);
            Gizmos.DrawLine(playerCheck.position, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.closeRangeAction));
            Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
        }

    }
}
