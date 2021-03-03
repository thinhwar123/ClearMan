using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPageUI : PageUI
{
    [SerializeField] private List<SaveSlotUI> saveSlotUIList;
    public override void CancelAction()
    {
        base.CancelAction();
    }

    public override void EndPage()
    {
        base.EndPage();
        for (int i = 0; i < saveSlotUIList.Count; i++)
        {
            saveSlotUIList[i].ResetPage();
        }
    }

    public override void Start()
    {
        base.Start();
        for (int i = 0; i < saveSlotUIList.Count; i++)
        {
            saveSlotUIList[i].ReadData();
        }
    }

    public override void StartPage()
    {
        base.StartPage();

    }

    public override void Update()
    {
        base.Update();
    }

    public override void UpdatePage()
    {
        base.UpdatePage();
    }
}
