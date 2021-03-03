using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SaveSlotUI : Selectable
{
    [SerializeField] public int saveSlotIndex;
    [SerializeField] private int curPageIndex;
    [SerializeField] private List<PageUI> pageList;
    public PlayerAttributeData playerAttributeData;
    public void ReadData()
    {
        playerAttributeData = new PlayerAttributeData();
        if (playerAttributeData.Load(saveSlotIndex))
        {
            ChangePage(1);

            UpdatePage(1);
            UpdatePage(2);
        }
        else
        {
            ChangePage(0);
        }
    }
    public void ResetPage()
    {
        if (curPageIndex == 2)
        {
            ChangePage(1);
        }
    }
    public void ChangePage(int pageIndex)
    {
        StartCoroutine(ChangePageCoroutine(pageIndex));
    }
    IEnumerator ChangePageCoroutine(int pageIndex)
    {
        pageList[curPageIndex].EndPage();
        curPageIndex = pageIndex;
        yield return new WaitForSecondsRealtime(0.3f);
        pageList[curPageIndex].StartPage();
        yield return new WaitForSecondsRealtime(0.3f);
    }
    public void UpdatePage(int pageIndex)
    {
        pageList[pageIndex].UpdatePage();
    }
}
