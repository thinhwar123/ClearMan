using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class CountSoulPointUI : MonoBehaviour
{
    [SerializeField] private float timeApear;
    [SerializeField] private TextMeshProUGUI curSoulPointText;
    [SerializeField] private TextMeshProUGUI countSoulPointText;
    private float curSoulPoint;
    private float curCountSoulPoint;
    private float maxCountSoulPoint;
    private Tween disappearTween1;
    private Tween disappearTween2;
    public void SetCurSoulPoint(float curSoulPoint)
    {
        this.curSoulPoint = curSoulPoint;
        curSoulPointText.text = curSoulPoint +"";
        countSoulPointText.text = "0";

        curCountSoulPoint = 0;
        maxCountSoulPoint = 0;
    }
    public void AddSoulPoint(int count)
    {
        Apear();
        maxCountSoulPoint += count;

        StopAllCoroutines();
        StartCoroutine(IncreaseSoulPointCount());
    }
    IEnumerator IncreaseSoulPointCount()
    {
        while (curCountSoulPoint < maxCountSoulPoint)
        {
            curCountSoulPoint++;
            countSoulPointText.text = curCountSoulPoint+ "";
            yield return new WaitForSecondsRealtime(0.1f);
        }

        yield return new WaitForSecondsRealtime(2f);
        countSoulPointText.GetComponent<CanvasGroup>().alpha = 0.5f;
        while (maxCountSoulPoint != 0)
        {
            curCountSoulPoint--;
            maxCountSoulPoint--;
            curSoulPoint++;
            countSoulPointText.text = curCountSoulPoint + "";
            curSoulPointText.text = curSoulPoint + "";
            yield return new WaitForSecondsRealtime(0.1f);
        }
        disappearTween1 = countSoulPointText.GetComponent<CanvasGroup>().DOFade(0, 2f);
        yield return new WaitForSecondsRealtime(timeApear);
        disappearTween2 = GetComponent<CanvasGroup>().DOFade(0, 2f);
    }
    public void Apear()
    {
        if (disappearTween1 != null)
        {
            disappearTween1.Kill();
        }
        if (disappearTween2 != null)
        {
            disappearTween2.Kill();
        }
        countSoulPointText.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
        GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }
}
