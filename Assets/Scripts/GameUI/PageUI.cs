using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
public class PageUI : MonoBehaviour
{
    [SerializeField] private bool canInteraction;
    private CanvasGroup canvasGroup;
    protected int inputX;
    protected int inputY;
    protected bool triggerButtonInput;
    protected bool cancelInput;
    public bool canCheckChangeButton;

    public Selectable startInteractionUI;
    public virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public virtual void Start()
    {

       
    }
    public virtual void StartPage()
    {
        canInteraction = true;
        canvasGroup.DOFade(1, 0.3f);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }
    public virtual void EndPage()
    {
        canInteraction = false;
        canvasGroup.DOFade(0, 0.3f);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    public virtual void Update()
    {        
        if (canInteraction && canCheckChangeButton)
        {
            inputX = DataGlobe.instance.inputHandler.changeButtonInputX;
            inputY = DataGlobe.instance.inputHandler.changeButtonInputY;
            triggerButtonInput = DataGlobe.instance.inputHandler.triggerButtonInput;
            cancelInput = DataGlobe.instance.inputHandler.cancelInput;

            CheckChangeButtonInput();
            CheckTriggeButtonInput();
            CheckCancel();
        }

    }
    public virtual void UpdatePage()
    {

    }
    public void CheckChangeButtonInput()
    {

        if ((inputX != 0 || inputY != 0) && EventSystem.current.currentSelectedGameObject == null && startInteractionUI != null )
        {
            DataGlobe.instance.inputHandler.UseChangeButtonInputX();
            DataGlobe.instance.inputHandler.UseChangeButtonInputY();
            EventSystem.current.SetSelectedGameObject(startInteractionUI.gameObject);
        }
    }
    public void CheckTriggeButtonInput()
    {
        if (EventSystem.current.currentSelectedGameObject != null && triggerButtonInput)
        {
            DataGlobe.instance.inputHandler.UseTriggerButtonInput();
            EventSystem.current.currentSelectedGameObject.GetComponent<InteractionUI>().TriggerButton();
        }
    }
    public void CheckCancel()
    {
        if (cancelInput)
        {
            Debug.Log("cancel");
            DataGlobe.instance.inputHandler.UseCancelInput();
            CancelAction();
        }
    }
    public virtual void CancelAction()
    {

    }
}
