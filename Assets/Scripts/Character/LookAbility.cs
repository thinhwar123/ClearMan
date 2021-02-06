using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAbility : CharacterAbility
{
    [SerializeField] private KeyCode InputLookUp;
    [SerializeField] private KeyCode InputLookDown;
    [SerializeField] private float holdTime;
    [SerializeField] private float curTime;
    [Header("References")]
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private JumpAbility jumpAbility;
    [Header("Stats")]
    [SerializeField] public bool isLook;
    protected override void Awake()
    {
        base.Awake();
        curTime = holdTime;
    }
    protected override void Update()
    {
        if ((Input.GetKey(InputLookUp) || Input.GetKey(InputLookDown)) && jumpAbility.isGround)
        {
            if (curTime < 0)
            {
                ani.SetFloat("lookDirection", characterMovement.directionMovement.y);
                ani.SetBool("isLook", true);
                isLook = true;
                Action();
            }
            else
            {
                curTime -= Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(InputLookUp) || Input.GetKeyUp(InputLookDown) || rb.velocity != Vector2.zero)
        {
            curTime = holdTime;
            ani.SetBool("isLook", false);
            isLook = false;
        }

    }
    protected override void Action()
    {
        
    }
}
