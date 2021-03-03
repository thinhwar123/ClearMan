using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class CreateNewDataButton : Selectable, InteractionUI
{
    [SerializeField] SaveSlotUI saveSlotUI;
    private Tween effectTween;
    [SerializeField] private CanvasGroup chooseEffect;

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
        DataGlobe.instance.CreateNewData(saveSlotUI.saveSlotIndex);
        DataGlobe.instance.SaveCurData();
        saveSlotUI.ReadData();
        //TODO: load scene ngay lap tuc
        //DataGlobe.instance.LoadScene("Character", true);
    }
}
