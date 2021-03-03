using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SoulwardUI : MonoBehaviour
{
    private Tween tween;
    private Tween[] fadeTweenList;
    private Tween[] scaleTweenList;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] public int type;
    [SerializeField] private List<CanvasGroup> typeList;

    public float appearTime = 0.1f;
    public float disappearTime = 0.1f;
    public float fadeTime = 0.1f;
    public float scaleTime = 0.1f;
    public void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        type = -1;
    }
    public void Appear()
    {
        tween = canvasGroup.DOFade(1, appearTime);
        fadeTweenList = new Tween[6];
        scaleTweenList = new Tween[6];
    }
    public void Disappear()
    {
        tween.Kill();
        tween = canvasGroup.DOFade(0, disappearTime);
        Destroy(gameObject, disappearTime);
    }
    public void ChooseType(int typeChoose)
    {

        if (typeChoose == type )
        {
            return;
        }
        if (type != -1)
        {
            fadeTweenList[type].Kill();
            scaleTweenList[type].Kill();
            fadeTweenList[type] = typeList[type].DOFade(0.5f, fadeTime);
            scaleTweenList[type] = typeList[type].transform.DOScale(2f, fadeTime);
        }

        if (typeChoose != -1)
        {
            fadeTweenList[typeChoose].Kill();
            scaleTweenList[typeChoose].Kill();
            fadeTweenList[typeChoose] = typeList[typeChoose].DOFade(1, scaleTime);
            scaleTweenList[typeChoose] = typeList[typeChoose].transform.DOScale(2.4f, scaleTime);
        }
        type = typeChoose;
        
    }
}
