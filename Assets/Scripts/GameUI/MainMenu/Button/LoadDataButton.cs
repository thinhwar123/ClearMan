using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
public class LoadDataButton : Selectable, InteractionUI
{
    [SerializeField] private SaveSlotUI saveSlotUI;
    [SerializeField] private CanvasGroup chooseEffect;
    private Tween effectTween;
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        effectTween.Kill();
        effectTween = chooseEffect.DOFade(0, 0.3f);
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
        effectTween = chooseEffect.DOFade(1, 0.3f);
    }

    public void TriggerButton()
    {
        Debug.Log("Loading .................");
        DataGlobe.instance.LoadData(saveSlotUI.saveSlotIndex);
        DataGlobe.instance.LoadScene("Character", true);
    }
}
