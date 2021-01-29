using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAndPullAbility : CharacterAbility
{
    [SerializeField] private LayerMask whatIsPhysicalObject;
    [SerializeField] private LayerMask whatIsBlockObject;
    [SerializeField] private float speed;
    private float normalSpeed;
    [Header("References")]
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject back;
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private bool isCatchObject;
    [SerializeField] public bool isPushOrPull;
    [SerializeField] private bool canPull;
    public Collider2D hit;
    protected override void Awake()
    {
        base.Awake();
        normalSpeed = characterMovement.speed;
    }
    protected override void Update()
    {        
        isCatchObject = Physics2D.OverlapBox(hand.transform.position, hand.GetComponent<BoxCollider2D>().size, 0, whatIsPhysicalObject);
        canPull = !Physics2D.OverlapBox(back.transform.position, back.GetComponent<BoxCollider2D>().size, 0, whatIsBlockObject);
        if (Input.GetKey(keyActive) && isUnlock && isCatchObject && jumpAbility.isGround)
        {
            //ani
            ani.SetBool("isCatchObject", true);
            if (rb.velocity.x !=0)
            {
                ani.SetFloat("pushDir", rb.velocity.normalized.x * transform.localScale.x);
            }
            
            isPushOrPull = true;
            LockAbility();
            hit = Physics2D.OverlapBox(hand.transform.position, hand.GetComponent<BoxCollider2D>().size, 0, whatIsPhysicalObject);

            characterMovement.isBlockFlip = true;            
            characterMovement.speed = speed;
            if (!canPull && rb.velocity.normalized.x * transform.localScale.x <= 0)
            {
                hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
            else
            {

                hit.GetComponent<Rigidbody2D>().velocity = new Vector2(characterMovement.directionMovement.x * characterMovement.speed, hit.GetComponent<Rigidbody2D>().velocity.y);
            }

        }
        else if ((Input.GetKeyUp(keyActive) && isPushOrPull ) || (!isCatchObject && hit))
        {
            ani.SetBool("isCatchObject", false);
            isPushOrPull = false;
            UnlockAbility();
            hit.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            hit = null;
            characterMovement.isBlockFlip = false;
            characterMovement.speed = normalSpeed;
        }
    }
    protected override void Action()
    {
        base.Action();
    }
   
}
