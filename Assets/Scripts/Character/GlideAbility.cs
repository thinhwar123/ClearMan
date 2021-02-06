using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideAbility : CharacterAbility
{
    [SerializeField] private float fallForce;

    [SerializeField] private float glideForce;
    private float normalGravity;
    [Header("References")]
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject windArea;
    [SerializeField] private LayerMask whatIsWind; 
    [SerializeField] private CharacterEffect characterEffect;
    [SerializeField] private JumpAbility jumpAbility;
    [SerializeField] private SwimAbility swimAbility;
    [SerializeField] private WallSlideAbility wallSlideAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private Vector2 glideDirection;
    [SerializeField] private bool isWind;
    [SerializeField] private bool isGlide;
    protected override void Awake()
    {
        base.Awake();
        normalGravity = rb.gravityScale;
    }
    protected override void Action()
    {

    }
    protected override void Update()
    {
        CheckWind();
        if (Input.GetKey(keyActive) && isUnlock && !jumpAbility.isGround && !swimAbility.isWater && !wallSlideAbility.isWallSlide && PlayerPrefs.GetInt("TripleJumpAbility") == 1)
        {
            LockAbility();
            isGlide = true;
            ani.SetBool("isGlide",true);
            rb.gravityScale = 0;
            if (isWind)
            {
                Glide();
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, -fallForce);
                characterMovement.isUnlock = true;
            }
        }
        if ((Input.GetKeyUp(keyActive) && PlayerPrefs.GetInt("TripleJumpAbility") == 1) || ((jumpAbility.isGround || swimAbility.isWater || wallSlideAbility.isWallSlide) && isGlide))
        {
            Debug.Log("abc");
            UnlockAbility();
            characterMovement.isUnlock = true;
            isGlide = false;
            ani.SetBool("isGlide", false);
            rb.gravityScale = normalGravity;
        }
    }
    public void Glide()
    {
        if (glideDirection == Vector2.up)
        {
            if (windArea.GetComponent<BoxCollider2D>().size.y > Mathf.Abs(transform.position.y - windArea.transform.position.y))
            {
                rb.velocity = new Vector2(rb.velocity.x, glideForce);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }
        else if (glideDirection == Vector2.left || glideDirection == Vector2.right)
        {

            if (windArea.GetComponent<BoxCollider2D>().size.y > Mathf.Abs(transform.position.x - windArea.transform.position.x))
            {
                rb.velocity = new Vector2( glideDirection.x * glideForce * 2, -fallForce);
                characterMovement.isUnlock = false;
            }
            else
            {
                rb.velocity = Vector2.zero;
                characterMovement.isUnlock = true;
            }
        }
    }
    public void CheckWind()
    {
        Collider2D hit = Physics2D.OverlapBox(body.transform.position, body.GetComponent<BoxCollider2D>().size, 0, whatIsWind);
        if (hit)
        {
            isWind = true;
            windArea = hit.gameObject;
            if (windArea.transform.eulerAngles == Vector3.zero)
            {
                glideDirection = Vector2.up;
            }
            else if (windArea.transform.eulerAngles == new Vector3(0, 0, 90))
            {
                glideDirection = Vector2.left;
            }
            else if (windArea.transform.eulerAngles == new Vector3(0, 0, -90))
            {
                glideDirection = Vector2.right;
            }
            else
            {
                Debug.Log(windArea.transform.eulerAngles);
            }
        }
        else
        {
            isWind = false;
            windArea = null;
        }
    }
}
