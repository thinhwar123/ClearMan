using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RunePageUI : PageUI
{
    [SerializeField] private List<Transform> tableZoneList;
    [SerializeField] private GameObject RuneItemUIPrefab;

    [SerializeField] private TextMeshProUGUI runeName;
    [SerializeField] private TextMeshProUGUI runeDescription;

    [Header("RuneAttribute")]
    [SerializeField] private TextMeshProUGUI curSoulRuneText;

    public override void EndPage()
    {
        base.EndPage();
    }

    public override void Start()
    {
        base.Start();
        //InstantiateTable();
    }

    public override void StartPage()
    {
        base.StartPage();

        runeName.text = "";
        runeDescription.text = "";
    }

    public override void UpdatePage()
    {
        base.UpdatePage();
        UpdateAttribute();
    }
    public void InstantiateTable()
    {
        startInteractionUI = null;
        for (int i = 0; i < tableZoneList.Count; i++)
        {
            for (int j = 0; j < DataGlobe.instance.runeTableList[i].tierList.Count; j++)
            {
                for (int k = 0; k < DataGlobe.instance.runeTableList[i].tierList[j].runeItemDataList.Count; k++)
                {
                    GameObject temp = Instantiate(RuneItemUIPrefab, tableZoneList[i]);
                    temp.GetComponent<RuneItemUI>().SetData(DataGlobe.instance.runeTableList[i].tierList[j].runeItemDataList[k], this);
                    if (startInteractionUI == null)
                    {
                        startInteractionUI = temp.GetComponent<Selectable>(); ;
                    }
                }
            }
        }
    }
    public void SetDescription(RuneStatus runeStatus, RuneItemData itemData)
    {
        if (runeStatus == RuneStatus.Lock)
        {
            runeName.text = "???";
            runeDescription.text = "You need unlock this rune first";
        }
        else
        {
            runeName.text = itemData.itemName;
            runeDescription.text = itemData.description;
        }
    }
    private void UpdateAttribute()
    {
        curSoulRuneText.text = DataGlobe.instance.playerAttributeData.curSoul + "";
    }
    public void UpdateRuneTable()
    {
        foreach (Transform tableZone in tableZoneList)
        {
            for (int i = 0; i < tableZone.childCount; i++)
            {
                Destroy(tableZone.GetChild(i).gameObject);
            }
        }
        InstantiateTable();
    }
}
