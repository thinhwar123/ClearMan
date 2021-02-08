using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : UIElement
{
    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                buttonIndex--;
                ChangeButton();
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                buttonIndex++;
                ChangeButton();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                curButton.TriggerButton();
            }
        }
    }
    public void ChangeButton()
    {
        if (buttonIndex >= buttons.Count)
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

    public override void StartUI()
    {
        base.StartUI();
        GetComponent<CanvasGroup>().alpha = 1;
    }

    public override void EndUI()
    {
        base.EndUI();
        GetComponent<CanvasGroup>().alpha = 0;
    }
}
