using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAbility : CharacterAbility
{
    [Header("AimConfig")]
    [SerializeField] private float timeScale;
    [SerializeField] private float timeAim;
    [SerializeField] private float distanceAim; // khoang cach tu soul den character
    [SerializeField] private float delayTimeAim;
    [SerializeField] private IEnumerator aiming;
    [Header("ShootConfig")]
    [SerializeField] private float speedShoot;
    [Header("References")]
    [SerializeField] public Soulward soulward;
    [SerializeField] public SoulWardAbility soulWardAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private bool canAim;
    [SerializeField] private bool isAim;
    [SerializeField] private bool canChangeAngle;
    protected override void Awake()
    {
        base.Awake();
        soulward = GameObject.Find("Soulward").GetComponent<Soulward>();

    }
    public void Start()
    {
        
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
            StartCoroutine(ResetPosition());
        } //Shoot
    }
    public Vector3 GetDirectionAim()
    {
        Vector3 mouseDirection = GetMousePosition() - transform.position;
        float angle = Vector3.SignedAngle(new Vector3(transform.localScale.x, 0, 0), mouseDirection, Vector3.forward);
        return mouseDirection;
    }
    public void ChangeAngle()
    {
        soulward.Aim(GetDirectionAim(), timeAim * timeScale, distanceAim, 5);
    }
    public void ShootSoulward()
    {
        soulward.soulwardObject.Shoot(GetDirectionAim(), 0, speedShoot, 5);
    }
    public Vector3 GetMousePosition()
    {
        Vector3 res = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector3(res.x, res.y, 0);
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
        canAim = true;
    }
    public IEnumerator ResetPosition()
    {
        soulward.soulwardObject.ResetPosition(delayTimeAim, 5);
        StartCoroutine(DelayAim());
        yield return new WaitForSeconds(delayTimeAim);
        soulWardAbility.isUnlock = true;
    }// bay ve
    public IEnumerator GetBack()
    {
        soulward.GetBack(delayTimeAim, 5);
        StartCoroutine(DelayAim());
        yield return new WaitForSeconds(delayTimeAim);
        soulWardAbility.isUnlock = true;

    }
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
