using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmDeleteDataButton : Button
{
    [SerializeField] protected ConfirmPanel confirmPanel;
    [SerializeField] protected CanvasGroup choosenEffect;
    public override void Choose()
    {
        base.Choose();
        GetComponent<CanvasGroup>().alpha = 1;
        choosenEffect.alpha = 1;
    }

    public override void Start()
    {
        base.Start();
    }

    public override void TriggerButton()
    {
        base.TriggerButton();
        DataManager.DeleteData(confirmPanel.slotSavePanel.indexSaveData);
        confirmPanel.ResetSlotSavePanel();
    }

    public override void UnChoose()
    {
        base.UnChoose();
        GetComponent<CanvasGroup>().alpha = 0.5f;
        choosenEffect.alpha = 0;
    }
}
