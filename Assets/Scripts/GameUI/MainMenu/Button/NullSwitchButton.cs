using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NullSwitchButton : Selectable, InteractionUI
{
    [SerializeField] private Selectable switchSelectableUI;
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        StartCoroutine(DelayChangePage());
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
    }

    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        StartCoroutine(DelayChangePage());
    }

    public void TriggerButton()
    {
    }

    IEnumerator DelayChangePage()
    {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(switchSelectableUI.gameObject);
    }
}
