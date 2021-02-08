using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPanel : UIElement
{
    [SerializeField] protected MainMenuUI mainMenu;
    public override void Start()
    {
        base.Start();
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
        curUIElement.EndUI();
        elementIndex = -1;
    }

    public override void Update()
    {
        base.Update();
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                elementIndex--;
                ChangeElement();
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                elementIndex++;
                ChangeElement();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                mainMenu.ChangeUIElement(0);
            }
        }
    }
    public void ChangeElement()
    {
        if (elementIndex >= elements.Count)
        {
            elementIndex = 0;
        }
        else if (elementIndex < 0)
        {
            elementIndex = elements.Count - 1;
        }
        curUIElement.EndUI();
        curUIElement = elements[elementIndex];
        curUIElement.StartUI();
    }
    public void ResetLoadPanel()
    {

    }
}
