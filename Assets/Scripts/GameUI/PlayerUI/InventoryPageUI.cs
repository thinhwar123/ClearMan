using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class InventoryPageUI : PageUI
{
    [SerializeField] private Transform abilityItemZone;
    [SerializeField] private GameObject abilityItemUIPrefab;
    [SerializeField] private Transform consumableItemZone;
    [SerializeField] private GameObject consumableItemUIPrefab;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;

    [Header("InventoryAttribute")]
    [SerializeField] private TextMeshProUGUI curHitPointInventoryText;
    [SerializeField] private TextMeshProUGUI curManaPointInventoryText;
    [SerializeField] private TextMeshProUGUI curSoulInventoryText;
    [SerializeField] private TextMeshProUGUI curGoldInventoryText;
    [SerializeField] private TextMeshProUGUI attackDamageInventoryText;
    public override void StartPage()
    {
        base.StartPage();


        itemName.text = "";
        itemDescription.text = "";
    }
    public override void EndPage()
    {
        base.EndPage();
    }
    public override void UpdatePage()
    {
        base.UpdatePage();
        startInteractionUI = null;

        UpdateAbilityItem();
        UpdateConsumableItem();
        UpdateAttribute();
    }
    public void SetDescription(ItemData itemData)
    {
        itemName.text = itemData.itemName;
        itemDescription.text = itemData.description;
    }
    public void UpdateAbilityItem()
    {
        for (int i = 0; i < abilityItemZone.childCount; i++)
        {
            Destroy(abilityItemZone.GetChild(i).gameObject);
        }
        for (int i = 0; i < DataGlobe.instance.abilityInventory.itemList.Count; i++)
        {
            GameObject temp = Instantiate(abilityItemUIPrefab, abilityItemZone);
            temp.GetComponent<AbilityItemUI>().SetData(DataGlobe.instance.abilityInventory.itemList[i].itemData, this);
            if (startInteractionUI == null)
            {
                startInteractionUI = temp.GetComponent<Selectable>();
            }
        }
    }
    public void UpdateConsumableItem()
    {
        for (int i = 0; i < consumableItemZone.childCount; i++)
        {
            Destroy(consumableItemZone.GetChild(i).gameObject);
        }
        for (int i = 0; i < DataGlobe.instance.consumableInventory.itemList.Count; i++)
        {
            GameObject temp = Instantiate(consumableItemUIPrefab, consumableItemZone);
            temp.GetComponent<ConsumableItemUI>().SetData(DataGlobe.instance.consumableInventory.itemList[i].itemData, this, DataGlobe.instance.consumableInventory.itemList[i].count);
            if (startInteractionUI == null)
            {
                startInteractionUI = temp.GetComponent<Selectable>();
            }
        }
    }
    public void UpdateAttribute()
    {
        curHitPointInventoryText.text = DataGlobe.instance.playerAttributeData.curHitPoint + "";
        curManaPointInventoryText.text = DataGlobe.instance.playerAttributeData.curManaPoint + "";
        curSoulInventoryText.text = DataGlobe.instance.playerAttributeData.curSoul + "";
        curGoldInventoryText.text = DataGlobe.instance.playerAttributeData.curGold + "";
        attackDamageInventoryText.text = DataGlobe.instance.playerAttributeData.curAttackDamage + "";
    }


}
