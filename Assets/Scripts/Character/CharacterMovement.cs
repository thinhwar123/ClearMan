using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;
    public Vector2 directionMovement;
    [SerializeField] public float speed;
    [SerializeField] public bool isUnlock;
    [SerializeField] public bool isBlockFlip;
    private void Awake()
    {
        ani = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        directionMovement.x = Input.GetAxisRaw("Horizontal");
        directionMovement.y = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        Flip();
        Run();
    }
    private void Flip()
    {
        if (isBlockFlip)
        {
            return;
        }
        if (directionMovement.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (directionMovement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void Run()
    {
        if (!isUnlock)
        {
            return;
        }
        if (directionMovement.x != 0)
        {
            ani.SetBool("isRun", true);            
        }
        else
        {
            ani.SetBool("isRun", false);
        }
        rb.velocity = new Vector2(directionMovement.x * speed, rb.velocity.y);
    }
}
