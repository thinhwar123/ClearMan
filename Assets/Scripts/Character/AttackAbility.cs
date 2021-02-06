using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAbility : CharacterAbility
{
    [SerializeField] private AttackDetails attackDetails;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float attackRadius;
    private float curCooldown;
    [Header("References")]
    [SerializeField] private CapsuleCollider2D attackPosition;
    [SerializeField] private CapsuleCollider2D attackPositionUp;
    [SerializeField] private CapsuleCollider2D attackPositionDown;
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private WallSlideAbility wallSlideAbility;
    [SerializeField] private LayerMask whatIsEnemy;
    [Header("Stats")]
    [SerializeField] private bool isAttack;

    protected override void Action()
    {
        if (curCooldown < 0)
        {

            if (Input.GetKey(KeyCode.W))
            {
                ani.SetTrigger("attackUp");
            }
            else if (!jumpAbility.isGround && Input.GetKey(KeyCode.S))
            {
                ani.SetTrigger("attackDown");
            }
            else if (wallSlideAbility.isWallSlide)
            {
                ani.SetTrigger("attackWallSlide");
            }
            else
            {
                ani.SetTrigger("attack");
            }
            attackDetails.position = transform.position;
            curCooldown = cooldownTime;
        }
    }
    protected override void Update()
    {
        base.Update();
        if (curCooldown >= 0)
        {
            curCooldown -= Time.deltaTime;
        }
    }
    public void TriggerAttack()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCapsuleAll(attackPosition.transform.position, attackPosition.size , attackPosition.direction, 0 ,whatIsEnemy);
        
        foreach (Collider2D coll in detectedObjects)
        {
            Debug.Log(coll.gameObject.name);
            coll.transform.parent.SendMessage("TakeDame", attackDetails);
        }
    }
    public void TriggerAttackUp()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCapsuleAll(attackPositionUp.transform.position, attackPositionUp.size, attackPositionUp.direction,0, whatIsEnemy);

        foreach (Collider2D coll in detectedObjects)
        {
            coll.transform.parent.SendMessage("TakeDame", attackDetails);
        }
    }
    public void TriggerAttackDown()
    {
        Collider2D[] detectedObjects = Physics2D.OverlapCapsuleAll(attackPositionDown.transform.position, attackPositionDown.size, attackPositionDown.direction, 0, whatIsEnemy);

        foreach (Collider2D coll in detectedObjects)
        {
            coll.transform.parent.SendMessage("TakeDame", attackDetails);
        }
    }
    public void TriggerAttackWallSlide()
    {
        Vector3 attackWallSlidePosition = new Vector3(-attackPosition.transform.position.x, attackPosition.transform.position.y, attackPosition.transform.position.z);
        Collider2D[] detectedObjects = Physics2D.OverlapCapsuleAll(attackWallSlidePosition, attackPosition.size, attackPosition.direction, 0, whatIsEnemy);

        foreach (Collider2D coll in detectedObjects)
        {
            coll.transform.parent.SendMessage("TakeDame", attackDetails);
        }
    }

}
