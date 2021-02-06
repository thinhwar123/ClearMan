using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulWardAbility : CharacterAbility
{
    [SerializeField] private float timeScale;
    [SerializeField] private float timeDelay;
    [SerializeField] private float safeDistance;
    [Header("References")]
    [SerializeField] private GameObject SoulwardUI;
    private GameObject tempSoulwardUI;
    [SerializeField] private GameObject canvas;
    [SerializeField] public Soulward soulward;
    [SerializeField] private List<CharacterAbility> listSoulwardAbility;
    [SerializeField] private CharacterMovement characterMovement;
    [Header("Stats")]
    [SerializeField] private int type;
    [SerializeField] private bool canChangeSoulWard;
    [SerializeField] private bool isSlowTime;
    [SerializeField] private Vector3 startMousePosition;
    [SerializeField] private int lastType;
    [SerializeField] private float cameraHeight;
    [SerializeField] private float cameraWidth;
    protected override void Awake()
    {
        base.Awake();
        soulward =  GameObject.Find("Soulward").GetComponent<Soulward>();

        cameraHeight = 2f * Camera.main.orthographicSize;
        cameraWidth = cameraHeight * Camera.main.aspect;
    }
    public void Start()
    {
        soulward.soulwardObject.ChangeType(type);
        for (int i = 0; i < listSoulwardAbility.Count; i++)
        {
            if (i == type)
            {
                PlayerPrefs.SetInt(listSoulwardAbility[i].GetType().Name, 1);
            }
            else
            {
                PlayerPrefs.SetInt(listSoulwardAbility[i].GetType().Name, 0);
            }
        }
        UnlockAbility();
    }
    protected override void Action()
    {

    }
    protected override void Update()
    {
        if (Input.GetKey(keyActive) && isUnlock && canChangeSoulWard)
        {
            LockAbility();
            isSlowTime = true;
            canChangeSoulWard = false;
            lastType = type;
            Time.timeScale = timeScale;
            if (tempSoulwardUI == null)
            {
                startMousePosition = GetMousePosition(true);
                tempSoulwardUI = Instantiate(SoulwardUI, startMousePosition, Quaternion.identity, canvas.transform);
                tempSoulwardUI.GetComponent<SoulwardUI>().Appear(timeDelay * timeScale);
            }
            
        }
        if (tempSoulwardUI != null)
        {
            tempSoulwardUI.GetComponent<SoulwardUI>().ChooseType(GetSoulType(), timeDelay * timeScale);
            ChangeType(GetSoulType());
        }
        if (Input.GetKeyUp(keyActive) && isSlowTime)
        {
            UnlockAbility();
            isSlowTime = false;
            StartCoroutine(Delay());
            Time.timeScale = 1;
            tempSoulwardUI.GetComponent<SoulwardUI>().Disappear(timeDelay);
            tempSoulwardUI = null;
        }
    }
    public int GetSoulType()
    {
        Vector3 mouseDirection = GetMousePosition(false) - startMousePosition;
        if (mouseDirection.magnitude < 1)
        {
            return -1;
        }
        else
        {
            float angle = Vector3.SignedAngle(Vector3.up, mouseDirection, Vector3.forward);
            int type = Mathf.RoundToInt((angle / 60) + 3f);
            if (type >= 5.5)
            {
                type = 0;
            }
            Debug.Log(type);
            return type;
        }
    }
    public void ChangeType(int typeChange)
    {        
        if (type == typeChange )
        {
            return;
        }
        else if (typeChange == -1)
        {
            type = lastType;
        }
        else
        {
            type = typeChange;
        }
        soulward.soulwardObject.ChangeType(type);
        for (int i = 0; i < listSoulwardAbility.Count; i++)
        {
            if (i == type)
            {
                PlayerPrefs.SetInt(listSoulwardAbility[i].GetType().Name, 1);
            }
            else
            {
                PlayerPrefs.SetInt(listSoulwardAbility[i].GetType().Name, 0);
            }
        }
    }
    public Vector3 GetMousePosition(bool isFix)
    {

        Vector3 res = Camera.main.ScreenToWorldPoint( Input.mousePosition);
        if (!isFix)
        {
            return new Vector3(res.x, res.y, 0);
        }
        if (cameraWidth / 2 - Mathf.Abs(res.x - Camera.main.transform.position.x) < safeDistance)
        {
            res = new Vector3((res.x - Camera.main.transform.position.x < 0 ? -1 : 1) * (cameraWidth / 2 - safeDistance) + Camera.main.transform.position.x, res.y, 0);
        }
        if (cameraHeight / 2 - Mathf.Abs(res.y - Camera.main.transform.position.y) < safeDistance)
        {
            res = new Vector3(res.x, (res.y - Camera.main.transform.position.y < 0 ? -1 : 1) * (cameraHeight / 2 - safeDistance) + Camera.main.transform.position.y, 0);
        }
        return new Vector3(res.x, res.y, 0);
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(timeDelay);
        canChangeSoulWard = true;
    }
}
