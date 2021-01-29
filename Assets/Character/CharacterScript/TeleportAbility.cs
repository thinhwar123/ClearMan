using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : CharacterAbility
{
    [Header("AimConfig")]
    [SerializeField] private float timeScale;
    [SerializeField] private float timeAim;
    [SerializeField] private float distanceAim; // khoang cach tu soul den character
    [SerializeField] private float delayTimeAim;
    [SerializeField] private IEnumerator aiming;
    [Header("ShootConfig")]
    [SerializeField] private float speedShoot;
    [SerializeField] private float maxDistance;
    [SerializeField] public IEnumerator delayTeleport;
    [Header("TeleportConfig")]
    [SerializeField] private float timeTeleport;
    [Header("GetBackConfig")]
    //[SerializeField] private float timeGetBack;
    [SerializeField] private KeyCode keyGetBack;
    [Header("References")]
    [SerializeField] public Soulward soulward;
    [SerializeField] public SoulWardAbility soulWardAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private bool canAim;
    [SerializeField] private bool isAim;
    [SerializeField] private bool canChangeAngle;
    [SerializeField] public bool canTeleport;
    [SerializeField] private bool isTeleprot;
    private float lastAngle;
    private float normalGravity;
    protected override void Awake()
    {
        base.Awake();
        normalGravity = rb.gravityScale;
        soulward = GameObject.Find("Soulward").GetComponent<Soulward>();
        
    }
    protected override void Action()
    {

    }
    protected override void Update()
    {
        if (Input.GetKey(keyActive) && isUnlock && canAim)
        {
            aiming = StartAim();
            StartCoroutine(aiming);
        } // Aim
        if (Input.GetKey(keyActive) && isUnlock && isAim)
        {
            if (canChangeAngle)
            {
                ChangeAngle();
            }
            else
            {
                StopAim();
                StartCoroutine(DelayAim());
                StartCoroutine(ResetPosition());
            }
        } // ChangeAim
        if (Input.GetKeyUp(keyActive) && isUnlock && isAim)
        {
            StopAim();
            ShootSoulward();
            delayTeleport = DelayTeleport();
            StartCoroutine(delayTeleport);
        } //Shoot
        if (canTeleport)
        {
            if (Input.GetKeyDown(keyActive) && isUnlock) 
            {
                StartCoroutine(Teleport());
            } //Tele
            else if (Input.GetKeyDown(keyGetBack) && isUnlock) 
            {
                StartCoroutine(GetBack());
            } //GetBack
        } 
    }
    public Vector3 GetDirectionAim()
    {
        Vector3 mouseDirection = GetMousePosition() - transform.position;
        float angle = Vector3.SignedAngle(new Vector3(transform.localScale.x, 0, 0), mouseDirection, Vector3.forward);
        return mouseDirection;
    }
    public void ChangeAngle()
    {
        soulward.Aim(GetDirectionAim(),timeAim * timeScale , distanceAim, 3);
    }
    public void ShootSoulward()
    {
        soulward.soulwardObject.Shoot(GetDirectionAim(),maxDistance, speedShoot, 3);
    }
    public Vector3 GetMousePosition()
    {
        Vector3 res = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(res.x, res.y, 0);
    }
    public void HitBlockObject()
    {
        StopCoroutine(delayTeleport);
        canTeleport = true;
    }
    public void StopAim()
    {
        StopCoroutine(aiming);
        UnlockAbility();
        isAim = false;
        canChangeAngle = false;
        Time.timeScale = 1;
        soulward.EndAim();
    }
    public IEnumerator DelayAim()
    {
        yield return new WaitForSeconds(delayTimeAim);
        canAim = true ;
    }
    public IEnumerator DelayTeleport()
    {
        yield return new WaitForSeconds(maxDistance /speedShoot);
        Debug.Log("Delay");
        canTeleport = true;
    }// khoang thoi gian tu luc ban cho den luc co the tele
    public IEnumerator Teleport()
    {
        LockAbility();
        canTeleport = false;
        characterMovement.isUnlock = false;
        rb.velocity = Vector2.zero;

        ani.SetTrigger("teleport");
        yield return new WaitForSeconds(timeTeleport);
        transform.position = soulward.soulwardObject.transform.position;
        soulward.soulwardObject.ResetPosition(delayTimeAim ,3);
        //StartCoroutine(GetBack());
        ani.SetTrigger("teleport");
        rb.gravityScale = 0;
        yield return new WaitForSeconds(timeTeleport);
        rb.gravityScale = normalGravity;
        UnlockAbility();
        soulWardAbility.isUnlock = true;
        characterMovement.isUnlock = true;
        StartCoroutine(DelayAim());
    }
    public IEnumerator GetBack() 
    {
        canTeleport = false;
        soulward.GetBack(delayTimeAim, 3);
        StartCoroutine(DelayAim());
        yield return new WaitForSeconds(delayTimeAim);
        soulWardAbility.isUnlock = true;

    }// tele ve
    public IEnumerator ResetPosition()
    {
        canTeleport = false;
        soulward.soulwardObject.ResetPosition(delayTimeAim, 3);
        StartCoroutine(DelayAim());
        yield return new WaitForSeconds(delayTimeAim);
        soulWardAbility.isUnlock = true;
    }// bay ve
    public IEnumerator StartAim()
    {
        LockAbility();
        soulWardAbility.isUnlock = false;
        isAim = true;
        canChangeAngle = true;
        canAim = false;
        Time.timeScale = timeScale;
        yield return new WaitForSecondsRealtime(timeAim);
        canChangeAngle = false;
    }
}
