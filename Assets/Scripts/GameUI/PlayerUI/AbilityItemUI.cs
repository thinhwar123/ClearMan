using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class AbilityItemUI : Selectable , InteractionUI
{
    [SerializeField] private Image itemImage;
    [SerializeField] private CanvasGroup chooseEffect;
    private Tween effectTween;
    private InventoryPageUI inventoryPageUI;
    public AbilityItemData itemData { get; private set; }

    public void SetData(ItemData itemData, InventoryPageUI inventoryUI)
    {
        this.itemData = (AbilityItemData)itemData;
        itemImage.sprite = itemData.icon;
        this.inventoryPageUI = inventoryUI;
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        effectTween.Kill();
        effectTween = chooseEffect.DOFade(1, 0.3f);
        inventoryPageUI.SetDescription(itemData);
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        effectTween.Kill();
        effectTween = chooseEffect.DOFade(0, 0.3f);
    }

    public void TriggerButton()
    {
        throw new System.NotImplementedException();
    }
}
