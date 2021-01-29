using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SoulwardUI : MonoBehaviour
{
    private Tween tween;
    private Tween[] listFadeTween;
    private Tween[] listScaleTween;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] public int type;
    [SerializeField] private List<CanvasGroup> listType;
    public void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        type = -1;
    }
    public void Appear(float time)
    {
        tween = canvasGroup.DOFade(1, time);
        listFadeTween = new Tween[6];
        listScaleTween = new Tween[6];
    }
    public void Disappear(float time)
    {
        tween.Kill();
        tween = canvasGroup.DOFade(0, time);
        Destroy(gameObject, time);
    }
    public void ChooseType(int typeChoose, float time)
    {

        if (typeChoose == type )
        {
            return;
        }
        if (type != -1)
        {
            listFadeTween[type].Kill();
            listScaleTween[type].Kill();
            listFadeTween[type] = listType[type].DOFade(0.5f, time / 2);
            listScaleTween[type] = listType[type].transform.DOScale(2f, time / 2);
        }

        if (typeChoose != -1)
        {
            listFadeTween[typeChoose].Kill();
            listScaleTween[typeChoose].Kill();
            listFadeTween[typeChoose] = listType[typeChoose].DOFade(1, time);
            listScaleTween[typeChoose] = listType[typeChoose].transform.DOScale(2.4f, time);
        }
        type = typeChoose;
        
    }
}
