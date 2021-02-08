using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPanel : UIElement
{
    [SerializeField] protected DeleteDataButton deleteDataButton;
    [SerializeField] public SlotSavePanel slotSavePanel;
    [SerializeField] protected LoadPanel loadPanel;
    public override void EndUI()
    {
        base.EndUI();
        buttonIndex = 0;
        ChangeButton();
        GetComponent<CanvasGroup>().alpha = 0;
        deleteDataButton.GetComponent<CanvasGroup>().alpha = 1;
        StartCoroutine(loadPanel.DelaySetActive(true));
        StartCoroutine(slotSavePanel.DelaySetActive(true));
    }

    public override void Start()
    {
        base.Start();

    }

    public override void DelayStartUI()
    {
        base.DelayStartUI();
        GetComponent<CanvasGroup>().alpha = 1;
        deleteDataButton.GetComponent<CanvasGroup>().alpha = 0;
        loadPanel.isActive = false;
        slotSavePanel.isActive = false;
    }

    public override void Update()
    {
        base.Update();
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                curButton.TriggerButton();
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                buttonIndex--;
                ChangeButton();
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                buttonIndex++;
                ChangeButton();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndUI();
            }
        }
    }
    public void ChangeButton()
    {
        if (buttonIndex >= buttons.Count )
        {
            buttonIndex = 0;
        }
        else if (buttonIndex < 0)
        {
            buttonIndex = buttons.Count - 1;
        }
        curButton.UnChoose();
        curButton = buttons[buttonIndex];
        curButton.Choose();
    }
    public void ResetSlotSavePanel()
    {
        EndUI();
        slotSavePanel.ResetSlotSavePanel(2);
    }
}
