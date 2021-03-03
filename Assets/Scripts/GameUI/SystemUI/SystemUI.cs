using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SystemUI : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float timeSetAttribute;
    [Range(0f, 1f)]
    [SerializeField] private float hitPointDangerzone;
    [Range(0f, 1f)]
    [SerializeField] private float manaDangerzone;

    [Header("PlayerIngameAttribute")]
    [SerializeField] private GameObject ingameAttribute;
    [SerializeField] private Image curHitPoint;
    [SerializeField] private TextMeshProUGUI curHitPointText;
    [SerializeField] private Image curManaPoint;
    [SerializeField] private TextMeshProUGUI curManaPointText;
    [SerializeField] private CountSoulPointUI countSoulPointUI;

    private bool isInHitPointDangerZone;
    private bool isInManaPointDangerZone;
    private Tween hitPointTween;
    private Tween manaPointTween;

    [Header("Inventory")] 
    [SerializeField] private BookUI bookUI;

    [Header("LoadScene")]
    [SerializeField] private CanvasGroup loadScene;

    [Header("System")]
    [SerializeField] private Transform itemPickUpNoticicationZone;
    [SerializeField] private GameObject notificationPrefab;
    private ItemPickUpNotificationUI goldNotification;

    public void UpdateAttributeUI()
    {
        ingameAttribute.GetComponent<CanvasGroup>().alpha = 1;
        curHitPoint.fillAmount = DataGlobe.instance.playerAttributeData.curHitPoint / DataGlobe.instance.playerAttributeData.maxHitPoint;
        curManaPoint.fillAmount = DataGlobe.instance.playerAttributeData.curManaPoint / DataGlobe.instance.playerAttributeData.maxManaPoint;
        curHitPointText.text = DataGlobe.instance.playerAttributeData.curHitPoint + "/" + DataGlobe.instance.playerAttributeData.maxHitPoint;
        curManaPointText.text = DataGlobe.instance.playerAttributeData.curManaPoint + "/" + DataGlobe.instance.playerAttributeData.maxManaPoint;
        countSoulPointUI.SetCurSoulPoint(DataGlobe.instance.playerAttributeData.curSoul);

        CheckHitPointDangerZone();
        CheckManaPointDangerZone();

        bookUI.UpdatePage(0);
        bookUI.UpdatePage(1);
    }
    public void SetHitPoint()
    {
        if (hitPointTween != null)
        {
            hitPointTween.Kill();
        }
        curHitPointText.text = DataGlobe.instance.playerAttributeData.curHitPoint + "/" + DataGlobe.instance.playerAttributeData.maxHitPoint;
        float lastAttribute = curHitPoint.fillAmount;
        float newAttribute = DataGlobe.instance.playerAttributeData.curHitPoint / DataGlobe.instance.playerAttributeData.maxHitPoint;
        hitPointTween = curHitPoint.DOFillAmount(newAttribute, Mathf.Abs(lastAttribute - newAttribute) * timeSetAttribute).SetEase(Ease.Linear);
        CheckHitPointDangerZone();
    }
    public void SetManaPoint()
    {
        if (manaPointTween != null)
        {
            manaPointTween.Kill();
        }
        curManaPointText.text = DataGlobe.instance.playerAttributeData.curManaPoint + "/" + DataGlobe.instance.playerAttributeData.maxManaPoint;
        float lastAttribute = curManaPoint.fillAmount;
        float newAttribute = DataGlobe.instance.playerAttributeData.curManaPoint / DataGlobe.instance.playerAttributeData.maxManaPoint;
        manaPointTween = curManaPoint.DOFillAmount(newAttribute, Mathf.Abs(lastAttribute - newAttribute) * timeSetAttribute).SetEase(Ease.Linear);
        CheckManaPointDangerZone();
    }
    public void CheckHitPointDangerZone()
    {
        if (DataGlobe.instance.playerAttributeData.curHitPoint / DataGlobe.instance.playerAttributeData.maxHitPoint < hitPointDangerzone)
        {
            isInHitPointDangerZone = true;
            StartCoroutine(StartHitPointDangerZone());
        }
        else
        {
            isInHitPointDangerZone = false;
        }
    }
    IEnumerator StartHitPointDangerZone()
    {
        while (isInHitPointDangerZone)
        {
            yield return new WaitForSeconds(0.3f);
            curHitPoint.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.3f);
            curHitPoint.color = Color.white;
        }
    }
    public void CheckManaPointDangerZone()
    {
        if (DataGlobe.instance.playerAttributeData.curManaPoint / DataGlobe.instance.playerAttributeData.maxManaPoint < manaDangerzone)
        {
            isInManaPointDangerZone = true;
            StartCoroutine(StartManaPointDangerZone());
        }
        else
        {
            isInManaPointDangerZone = false;
        }
    }
    IEnumerator StartManaPointDangerZone()
    {
        while (isInManaPointDangerZone)
        {
            yield return new WaitForSeconds(0.3f);
            curManaPoint.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.3f);
            curManaPoint.color = Color.white;
        }
    }
    public void OpenBook()
    {
        bookUI.OpenBookUI();
    }
    public void CloseBook()
    {
        bookUI.CloseBookUI();
    }
    public void UpdatePage(int pageIndex)
    {
        bookUI.UpdatePage(pageIndex);
    }
    public void PickUp(ItemData itemData, int count)
    {
        if (itemData.type == ItemType.Ability)
        {
            GameObject temp = Instantiate(notificationPrefab, itemPickUpNoticicationZone);
            temp.GetComponent<ItemPickUpNotificationUI>().StartNotification(itemData, count);
            UpdatePage(0);
        }
        else if (itemData.type == ItemType.Consumable)
        {
            GameObject temp = Instantiate(notificationPrefab, itemPickUpNoticicationZone);
            temp.GetComponent<ItemPickUpNotificationUI>().StartNotification(itemData, count);
            UpdatePage(0);
        }
        else if (itemData.type == ItemType.Gold)
        {
            if (goldNotification == null )
            {
                goldNotification = Instantiate(notificationPrefab, itemPickUpNoticicationZone).GetComponent<ItemPickUpNotificationUI>();
                goldNotification.StartNotification(itemData, count);
            }
            else if (!goldNotification.canStack)
            {
                goldNotification = Instantiate(notificationPrefab, itemPickUpNoticicationZone).GetComponent<ItemPickUpNotificationUI>();
                goldNotification.StartNotification(itemData, count);
            }
            else 
            {
                goldNotification.AddMaxCount(count);
            }
            UpdatePage(0);
        }
        else if (itemData.type == ItemType.Soul)
        {
            countSoulPointUI.AddSoulPoint(count);
            UpdatePage(0);
            UpdatePage(1);
        }

    }
    public void StartLoadScene()
    {
        loadScene.DOFade(1, 1f);
        loadScene.interactable = true;
        loadScene.blocksRaycasts = true;
    }
    public void StopLoadScene()
    {
        loadScene.DOFade(0, 1f);
        loadScene.interactable = false;
        loadScene.blocksRaycasts = false;
    }


    #region DebugFunction
    public void UpdateRuneTable()
    {
        bookUI.UpdateRuneTable();
    }
    #endregion
}
