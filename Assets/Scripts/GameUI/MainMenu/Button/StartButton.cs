using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class StartButton : Button
{
    private Tween tween;
    [SerializeField] MainMenuUI mainMenuUI;
    public override void Choose()
    {
        base.Choose();
        tween.Kill();
        tween = buttonImage.DOFade(1, 0.1f);
    }

    public override void TriggerButton()
    {
        base.TriggerButton();
        mainMenuUI.ChangeUIElement(1);
    }

    public override void UnChoose()
    {
        base.UnChoose();
        tween.Kill();
        tween = buttonImage.DOFade(100f/255f, 0.1f);
    }
}
