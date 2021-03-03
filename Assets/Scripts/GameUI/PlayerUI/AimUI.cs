using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class AimUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Image timeCouner1;
    [SerializeField] private Image timeCouner2;

    public float timeActive ;
    public float normalAlpha ;
    public Ease ease;

    public void Awake()
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(normalAlpha, timeActive);
    }
    public void Start()
    {
        
    }
    public void Update()
    {
        transform.position = DataGlobe.instance.player.transform.position;
    }
    public void ChangeDirection(Vector3 direction)
    {
        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, direction, Vector3.forward));
    }
    public void StartAim(float time)
    {
        timeCouner1.DOFillAmount(0, time).SetEase(ease);
        timeCouner2.DOFillAmount(0, time).SetEase(ease);
    }
    public void EndAim()
    {
        canvasGroup.DOFade(0, timeActive);
        GameObject.Destroy(gameObject, timeActive);
    }
}
