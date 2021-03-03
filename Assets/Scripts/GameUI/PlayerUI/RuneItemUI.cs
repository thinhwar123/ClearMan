using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class RuneItemUI : Selectable , InteractionUI
{
    [SerializeField] private Image runeImage;
    [SerializeField] private CanvasGroup chooseEffect;
    [SerializeField] private CanvasGroup LockEffect;
    [SerializeField] private CanvasGroup DeactiveEffect;
    [SerializeField] private List<CanvasGroup> LineRequirement;

    public RuneItemData itemData { get; private set; }
    private RuneStatus curRuneStatus;
    private Tween effectTween;
    private RunePageUI runePageUI;
    private bool isCount = false;
    public void SetData(RuneItemData itemData, RunePageUI runePageUI)
    {
        this.itemData = itemData;
        runeImage.sprite = itemData.icon;
        this.runePageUI = runePageUI;
        curRuneStatus = DataGlobe.instance.GetRuneStatus(itemData.table, itemData.tier, itemData.index);
        switch (curRuneStatus)
        {
            case RuneStatus.Lock:
                LockEffect.alpha = 1;
                DeactiveEffect.alpha = 1;
                runeImage.color = Color.clear;
                break;

            case RuneStatus.Deactive:
                LockEffect.alpha = 0;
                DeactiveEffect.alpha = 1;
                runeImage.color = Color.white;
                break;

            case RuneStatus.Active:
                LockEffect.alpha = 0;
                DeactiveEffect.alpha = 0;
                runeImage.color = Color.white;
                break;

            case RuneStatus.Hidden:
                GetComponent<CanvasGroup>().alpha = 0;
                GetComponent<CanvasGroup>().interactable = false;
                break;

            default:
                break;
        }

        foreach (RuneRequire runeRequire in this.itemData.runeRequireList)
        {
            LineRequirement[(int)runeRequire.runeRequireOffsetPosition].alpha = 1;
        }
    }
    public void TriggerButton()
    {
        if (CheckRequire())
        {
            ActiveRune();
        }
    }
    public bool CheckRequire()
    {
        bool runeCheck = itemData.runeRequireList.Count == 0 ? true : false;
        foreach (RuneRequire runeRequire in itemData.runeRequireList)
        {
            RuneStatus runeRequireStatus = DataGlobe.instance.GetRuneStatus(runeRequire.runeRequireData.table, runeRequire.runeRequireData.tier, runeRequire.runeRequireData.index);
            if (runeRequireStatus == RuneStatus.Active)
            {
                runeCheck = true;
                break;
            }
        }
        return runeCheck && curRuneStatus == RuneStatus.Deactive && DataGlobe.instance.playerAttributeData.curSoul >= itemData.soulRequire;
    }
    public void ActiveRune()
    {
        curRuneStatus = RuneStatus.Active;
        DataGlobe.instance.SetRuneStatus(itemData.table, itemData.tier, itemData.index, RuneStatus.Active);
        LockEffect.alpha = 0;
        DeactiveEffect.alpha = 0;
        runeImage.color = Color.white;

        DataGlobe.instance.playerAttributeData.curSoul -= itemData.soulRequire;
        DataGlobe.instance.systemUI.UpdateAttributeUI();
    }
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        effectTween.Kill();
        effectTween = chooseEffect.DOFade(1, 0.3f);
        runePageUI.SetDescription(DataGlobe.instance.GetRuneStatus(itemData.table, itemData.tier, itemData.index), itemData);
    }
    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        effectTween.Kill();
        effectTween = chooseEffect.DOFade(0, 0.3f);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (curRuneStatus == RuneStatus.Deactive)
        {
            StartCoroutine(Hold());
        }
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (isCount)
        {
            isCount = false;
            StopAllCoroutines();
        }
    }
    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (isCount)
        {
            isCount = false;
            StopAllCoroutines();
        }
    }
    IEnumerator Hold()
    {
        isCount = true;
        yield return new WaitForSecondsRealtime(2f);
        TriggerButton();
    }
}
