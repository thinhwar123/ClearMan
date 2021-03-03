using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGamePageUI : PageUI
{
    [SerializeField] private MainMenuUI mainMenuUI;
    public override void CancelAction()
    {
        base.CancelAction();
        mainMenuUI.ChangePage(0);
    }
}
