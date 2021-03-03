using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ConsumableItemUI : Selectable , InteractionUI
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private CanvasGroup chooseEffect;
    private Tween effectTween;
    private InventoryPageUI inventoryUI;
    public ConsumableItemData itemData { get; private set; }
    public void SetData(ItemData itemData, InventoryPageUI inventoryUI, int count)
    {
        this.itemData = (ConsumableItemData)itemData;
        itemImage.sprite = itemData.icon;
        countText.text = count + "";
        this.inventoryUI = inventoryUI;
    }

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
        inventoryUI.SetDescription(itemData);
    }

    public void TriggerButton()
    {

    }
}
