using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : UIElement
{
    public override void EndUI()
    {
        base.EndUI();
    }

    public override void Start()
    {
        base.Start();

    }

    public override void StartUI()
    {
        base.StartUI();
    }

    public override void Update()
    {
        base.Update();
    }
    public void ChangeUIElement(int indexUIElement)
    {
        curUIElement.EndUI();
        curUIElement = elements[indexUIElement];
        curUIElement.StartUI();
    }
}
