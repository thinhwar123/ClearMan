using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
public class CancelDeleteDatabButton : Selectable, InteractionUI
{
    [SerializeField] private SaveSlotUI saveSlotUI;
    [SerializeField] private Image icon;
    private Tween effectTween;
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        effectTween.Kill();
        effectTween = icon.GetComponent<RectTransform>().DOScale(1, 0.3f);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        TriggerButton();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        effectTween.Kill();
        effectTween = icon.GetComponent<RectTransform>().DOScale(1.3f, 0.3f);
    }
    public void TriggerButton()
    {
        saveSlotUI.ChangePage(1);
    }

}
