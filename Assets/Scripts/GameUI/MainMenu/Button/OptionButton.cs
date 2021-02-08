using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class OptionButton : Button
{
    private Tween tween;
    public override void Choose()
    {
        base.Choose();
        tween.Kill();
        tween = buttonImage.DOFade(1, 0.1f);
    }

    public override void TriggerButton()
    {
        base.TriggerButton();
    }

    public override void UnChoose()
    {
        base.UnChoose();
        tween.Kill();
        tween = buttonImage.DOFade(100f / 255f, 0.1f);
    }
}
