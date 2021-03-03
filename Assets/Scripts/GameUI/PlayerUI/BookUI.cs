using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class BookUI : MonoBehaviour
{
    [SerializeField] private int curPageIndex;
    [SerializeField] private List<PageUI> pageList;

    private int changePageInput;
    private Animator anim;
    private bool isOpenBook;
    public bool isActive { get; private set; }
    private void Start()
    {
        isActive = false;
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        ChangePage();
    }
    public void OpenBookUI()
    {
        curPageIndex = 0; //Sua lai startPage
        isOpenBook = true;
        anim.SetBool("openBook", true);
    }
    public void CloseBookUI()
    {
        isOpenBook = false;
        anim.SetBool("openBook", false);
    }
    public void ChangePage()
    {
        changePageInput = DataGlobe.instance.inputHandler.changePageInput;
        if (isActive && changePageInput != 0)
        {
            StartCoroutine(ChangePageCoroutine(changePageInput));
        }
    }
    public void AnimationTrigger() 
    {
        if (isOpenBook)
        {
            pageList[curPageIndex].StartPage();
        }
        else
        {
            isActive = false;
            pageList[curPageIndex].EndPage();

        }
    }
    public void  AnimationFinish()
    {
        if (isOpenBook)
        {
            isActive = true;
        }
    }
    IEnumerator ChangePageCoroutine(int changePageInput)
    {
        isActive = false;
        pageList[curPageIndex].EndPage();
        curPageIndex = (curPageIndex + pageList.Count + changePageInput) % pageList.Count;        
        yield return new WaitForSecondsRealtime(0.3f);
        pageList[curPageIndex].StartPage();
        yield return new WaitForSecondsRealtime(0.3f);
        isActive = true;
    }
    public void UpdatePage(int pageIndex)
    {
        pageList[pageIndex].UpdatePage();
    }
    public void UpdateRuneTable()
    {
        ((RunePageUI)pageList[1]).UpdateRuneTable();
    }
}
