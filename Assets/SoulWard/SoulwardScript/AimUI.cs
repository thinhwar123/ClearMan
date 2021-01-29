using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class AimUI : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private float timeActive;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float normalAlpha;
    [SerializeField] private Image timeCouner1;
    [SerializeField] private Image timeCouner2;
    [SerializeField] private Ease ease;

    public void Awake()
    {
        target = GameObject.Find("Character");
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(normalAlpha, timeActive);
    }
    public void Start()
    {
        
    }
    public void Update()
    {
        transform.position = target.transform.position;
    }
    public void ChangeDirection(Vector3 direction)
    {
        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, direction, Vector3.forward));
    }
    public void StartAim(float time)
    {
        timeCouner1.DOFillAmount(0, time).SetEase(this.ease);
        timeCouner2.DOFillAmount(0, time).SetEase(this.ease);
    }
    public void EndAim()
    {
        canvasGroup.DOFade(0, timeActive);
        GameObject.Destroy(gameObject, timeActive);
    }
}
