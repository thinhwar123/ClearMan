using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotSavePanel : UIElement
{
    [SerializeField] public int indexSaveData;
    [SerializeField] protected List<Sprite> sprites;
    [SerializeField] protected bool isEmty;
    [SerializeField] protected SaveData saveData;
    [SerializeField] protected CanvasGroup newGameButton;
    [SerializeField] protected CanvasGroup characterAttribute;
    [SerializeField] protected RectTransform indexArea;
    [SerializeField] protected RectTransform deleteArea;
    [SerializeField] protected TextMeshProUGUI health;
    [SerializeField] protected TextMeshProUGUI mana;
    [SerializeField] protected TextMeshProUGUI gold;
    [SerializeField] protected TextMeshProUGUI soul;
    [SerializeField] protected TextMeshProUGUI map;
    [SerializeField] protected TextMeshProUGUI hours;
    [SerializeField] protected TextMeshProUGUI minutes;

    public override void EndUI()
    {
        base.EndUI();
        GetComponent<CanvasGroup>().alpha = 0.7f;
        if (!isEmty)
        {
            buttonIndex = 0;
            ChangeButton();
        }
        else
        {
            buttonIndex = 2;
        }
        curButton.UnChoose();
    }

    public override void Start()
    {
        base.Start();
        ReadData();
    }
    public void ReadData()
    {
        saveData = DataManager.ReadData(indexSaveData);
        if (saveData == null)
        {
            newGameButton.alpha = 1;
            characterAttribute.alpha = 0;
            isEmty = true;
        }
        else
        {
            newGameButton.alpha = 0;
            characterAttribute.alpha = 1;
            isEmty = false;
            health.text = saveData.curHealth +"";
            mana.text = saveData.curMana + "";
            gold.text = saveData.curGold + "";
            soul.text = saveData.curSoul + "";
            map.text = saveData.curMap;
            hours.text = saveData.curHours + "";
            minutes.text = saveData.curMinutes + "";
        }
    }
    public override void StartUI()
    {
        base.StartUI();
        if (isEmty)
        {
            curButton = buttons[2];
            buttonIndex = 2;
        }
        curButton.Choose();
        GetComponent<CanvasGroup>().alpha = 1;
    }

    public override void Update()
    {
        base.Update();
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                curButton.TriggerButton();
            }
            else if(!isEmty)
            {
                if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    buttonIndex--;
                    ChangeButton();
                }
                else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    buttonIndex++;
                    ChangeButton();
                }
            }            
        }
    }
    public void ChangeButton()
    {
        if (buttonIndex >= buttons.Count-1)
        {
            buttonIndex = 0;
        }
        else if (buttonIndex < 0)
        {
            buttonIndex = buttons.Count - 2;
        }
        GetComponent<Image>().sprite = sprites[buttonIndex];
        if (buttonIndex == 0)
        {
            indexArea.localPosition = new Vector3(-651f, 22f, 0);
            deleteArea.localPosition = new Vector3(1228f, -34, 0);
        }
        else if (buttonIndex == 1)
        {
            indexArea.localPosition = new Vector3(-651f, -18, 0);
            deleteArea.localPosition = new Vector3(1228f, 53, 0);
        }

        curButton.UnChoose();
        curButton = buttons[buttonIndex];
        curButton.Choose();
    }
    public void ResetSlotSavePanel(int indexButton)
    {
        ReadData();
        buttonIndex = 0;
        ChangeButton();
        curButton = buttons[indexButton];
        buttonIndex = indexButton;
        curButton.Choose();
    }
}
