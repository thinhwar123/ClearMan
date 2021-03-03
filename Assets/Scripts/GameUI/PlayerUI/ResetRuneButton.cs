using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class ResetRuneButton : Selectable, InteractionUI
{
    private Tween effectTween;
    [SerializeField] private CanvasGroup chooseEffect;
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        effectTween.Kill();
        effectTween = chooseEffect.DOFade(0, 0.3f);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        effectTween.Kill();
        effectTween = chooseEffect.DOFade(1, 0.3f);
    }
    public void TriggerButton()
    {
        DataGlobe.instance.ResetRune();
    }

}
