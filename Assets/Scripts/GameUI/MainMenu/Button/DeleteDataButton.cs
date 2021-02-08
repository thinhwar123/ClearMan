using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteDataButton : Button
{
    [SerializeField] protected CanvasGroup choosenEffect;
    [SerializeField] protected SlotSavePanel slotSavePanel;
    [SerializeField] protected ConfirmPanel confirmPanel;
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
        confirmPanel.DelayStartUI();
    }

    public override void UnChoose()
    {
        base.UnChoose();
        choosenEffect.alpha = 0;
    }
}
