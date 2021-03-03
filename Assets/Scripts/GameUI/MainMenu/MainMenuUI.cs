using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private int curPageIndex;
    [SerializeField] private List<PageUI> pageList;

    public bool isActive { get; private set; }
    private void Start()
    {
        isActive = false;
        curPageIndex = 0;
        DataGlobe.instance.inputHandler.ChangeActionMap("UI");
    }
    public void ChangePage(int pageIndex)
    {
        StartCoroutine(ChangePageCoroutine(pageIndex));
    }
    IEnumerator ChangePageCoroutine(int pageIndex)
    {
        isActive = false;
        pageList[curPageIndex].EndPage();
        curPageIndex = pageIndex;
        yield return new WaitForSecondsRealtime(0.3f);
        pageList[curPageIndex].StartPage();
        yield return new WaitForSecondsRealtime(0.3f);
        isActive = true;
    }
    public void UpdatePage(int pageIndex)
    {
        pageList[pageIndex].UpdatePage();
    }
}
