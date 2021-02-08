using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateNewDataButton : Button
{
    [SerializeField] protected CanvasGroup choosenEffect;
    [SerializeField] protected SlotSavePanel slotSavePanel;
    public override void Choose()
    {
        base.Choose();
        choosenEffect.alpha = 1;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void TriggerButton()
    {
        base.TriggerButton();
        DataManager.WriteData(new SaveData(), slotSavePanel.indexSaveData);
        slotSavePanel.ResetSlotSavePanel(0);
    }

    public override void UnChoose()
    {
        base.UnChoose();
        choosenEffect.alpha = 0;
    }
}
