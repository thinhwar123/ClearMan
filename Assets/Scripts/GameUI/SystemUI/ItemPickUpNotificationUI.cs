using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ItemPickUpNotificationUI : Selectable
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemNameTetx;
    [SerializeField] private TextMeshProUGUI countText;
    private bool onClick;
    public int maxCount { get; private set; }
    public int curCount { get; private set; }
    public bool canStack { get; private set; }
    public bool isIncrease { get; private set; }
    public bool canIncrease { get; private set; }
    public void StartNotification(ItemData itemData, int count)
    {
        curCount = 0;
        maxCount = 0;
        canStack = true;
        isIncrease = false;
        canIncrease = false;

        icon.sprite = itemData.icon;
        itemNameTetx.text = itemData.itemName;

        if (itemData.type == ItemType.Consumable)
        {
            maxCount = count;
            countText.text = "x0";
        }
        else if (itemData.type == ItemType.Gold)
        {
            maxCount = count;
            countText.text = "x0";
        }
        GetComponent<Animator>().SetTrigger("notification");
    }
    public void AddMaxCount(int count) // them so luong , dem so luong hien tai va so luong max
    {
        maxCount += count;
        if (!isIncrease && canIncrease)
        {
            StartCoroutine(IncreaseCount());
        }
    }
    public void StopStack()
    {
        canStack = false;
    }
    public void AnimationFinish()
    {
        Destroy(gameObject, 0);
    }
    
    public void AnimationTrigger()
    {
        StartCoroutine(IncreaseCount());
        GetComponent<Animator>().SetTrigger("notificationRepeat");
        canIncrease = true;
    }
    private Tween temp;
    IEnumerator IncreaseCount()
    {
        isIncrease = true;
        while (curCount < maxCount)
        {
            GetComponent<Animator>().SetTrigger("notificationRepeat");
            yield return new WaitForSeconds(0.02f);
            curCount++;
            countText.text = "x" + curCount;
            yield return new WaitForSeconds(0.02f);
        }
        isIncrease = false;
    }
}
