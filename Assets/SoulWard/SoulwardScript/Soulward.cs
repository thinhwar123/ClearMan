using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Experimental.Rendering.Universal;
public class Soulward : MonoBehaviour
{
    [SerializeField] public GameObject target;
    [SerializeField] public SoulwardObject soulwardObject;
    [SerializeField] private GameObject soulwardPrefab;
    [SerializeField] private GameObject aimUIPrefab;
    [SerializeField] private GameObject aimUI;
    public Tween tweenZ;
    private void Awake()
    {
        target = GameObject.Find("Character");
        soulwardObject = GetComponentInChildren<SoulwardObject>();
        tweenZ = transform.DORotate(new Vector3(0, 0, 45), 6).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
    public void Start()
    {

    }
    void Update()
    {
        Follow();
    }
    public void Follow()
    {
        transform.position = target.transform.position;
    }
    public void Aim(Vector3 direction, float time, float distanceAim, int type)
    {
        if (aimUI == null)
        {
            aimUI = Instantiate(aimUIPrefab, transform.position, Quaternion.identity, GameObject.Find("Canvas").transform);
            aimUI.GetComponent<AimUI>().StartAim(time);
        }
        aimUI.GetComponent<AimUI>().ChangeDirection(direction);
        soulwardObject.Aim(direction, distanceAim, type);
    }
    public void EndAim()
    {
        aimUI.GetComponent<AimUI>().EndAim();
        aimUI = null;
    }
    public void GetBack(float timeGetBack, int type)
    {
        soulwardObject.Disappear(timeGetBack);
        GameObject temp = Instantiate(soulwardPrefab, transform.position + Vector3.right * soulwardObject.distance, Quaternion.identity, transform);
        soulwardObject = temp.GetComponent<SoulwardObject>();
        soulwardObject.Appear(timeGetBack, type);
    }
}
