using System.Collections;
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
    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] private bool isDetectingWall;
    [SerializeField] private bool isDetectingLedge;
    private Vector2 velocityWorkspace;
    public virtual void Start()
    {
        facingDirection = -1;
        aliveGO = transform.Find("AliveGO").gameObject;
        rb = aliveGO.GetComponent<Rigidbody2D>();
        ani = aliveGO.GetComponent<Animator>();
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
    public virtual void SetVelocity(float velocity)
    {
        velocityWorkspace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkspace;
    }
    public virtual bool CheckWall()
    {
        isDetectingWall = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, entityData.wallCheckDistance, entityData.whatIsWall);
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, entityData.wallCheckDistance, entityData.whatIsWall);
    }
    public virtual bool CheckLedge()
    {
        isDetectingLedge = Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
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
    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0f, 180f, 0f);
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(playerCheck.position + Vector3.up * 0.1f, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.minAgroDistance) + Vector3.up * 0.1f);
        Gizmos.DrawLine(playerCheck.position + Vector3.down * 0.1f, playerCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.maxAgroDistance) + Vector3.down * 0.1f);
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down  * entityData.ledgeCheckDistance));
    }
}
