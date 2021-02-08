using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] protected bool isActive;
    [SerializeField] protected List<Button> buttons;
    protected Button curButton;
    protected int buttonIndex;
    public virtual void Start()
    {
        curButton = buttons[0];
        buttonIndex = 0;
        curButton.Choose();
    }
    public virtual void Update()
    {

    }
    public virtual void StartPanel()
    {
        isActive = true;
    }
    public virtual void EndPanel()
    {
        isActive = false;
    }
    public virtual void nextButton()
    {
        buttonIndex++;
        if (buttonIndex >= buttons.Count)
        {
            buttonIndex -= buttons.Count;
        }
        curButton.UnChoose();
        curButton = buttons[buttonIndex];
        curButton.Choose();
    }
    public virtual void backButton()
    {
        buttonIndex--;
        if (buttonIndex < 0)
        {
            buttonIndex += buttons.Count;
        }
        curButton.UnChoose();
        curButton = buttons[buttonIndex];
        curButton.Choose();
    }
}
