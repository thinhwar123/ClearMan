using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
public class SaveGamePageUI : PageUI
{
    [SerializeField] private SaveSlotUI saveSlotUI;
    [SerializeField] private TextMeshProUGUI curHitPoint;
    [SerializeField] private TextMeshProUGUI curManaPoint;
    [SerializeField] private TextMeshProUGUI curSoul;
    [SerializeField] private TextMeshProUGUI curGold;
    [SerializeField] private TextMeshProUGUI curMap;
    [SerializeField] private TextMeshProUGUI curPlayTime;
    [SerializeField] private MainMenuUI mainMenuUI;
    public override void CancelAction()
    {
        base.CancelAction();
        mainMenuUI.ChangePage(0);
    }
    public override void UpdatePage()
    {
        base.UpdatePage();
        curHitPoint.text = saveSlotUI.playerAttributeData.curHitPoint + "/" + saveSlotUI.playerAttributeData.maxHitPoint;
        curManaPoint.text = saveSlotUI.playerAttributeData.curManaPoint + "/" + saveSlotUI.playerAttributeData.maxManaPoint;
        curSoul.text = saveSlotUI.playerAttributeData.curSoul + "/" + saveSlotUI.playerAttributeData.maxSoul;
        curGold.text = saveSlotUI.playerAttributeData.curGold + "";
        curMap.text = saveSlotUI.playerAttributeData.curMap;
        curPlayTime.text = saveSlotUI.playerAttributeData.curHours + "h" + saveSlotUI.playerAttributeData.curMinutes + "m";
    }
    public override void StartPage()
    {
        base.StartPage();

    }
}
