using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElement : MonoBehaviour
{
    [SerializeField] public bool isActive;
    [SerializeField] 
    protected List<UIElement> elements;
    [SerializeField] 
    protected List<Button> buttons;
    //[SerializeField] 
    protected int elementIndex;
    //[SerializeField]
    protected UIElement curUIElement;
    //[SerializeField] 
    protected int buttonIndex;
    //[SerializeField] 
    protected Button curButton;
    public virtual void Start()
    {
        elementIndex = -1;
        buttonIndex = -1;
        if (isActive)
        {
            StartUI();
        }
    }
    public virtual void Update()
    {
        
    }
    public virtual void StartUI()
    {
        isActive = true;
        if (elements.Count != 0  && elementIndex == -1)
        {
            curUIElement = elements[0];
            curUIElement.StartUI();
            elementIndex = 0;
        }
        if (buttons.Count != 0 && buttonIndex == -1 )
        {
            curButton = buttons[0];
            curButton.Choose();
            buttonIndex = 0;
        }
    }
    public virtual void DelayStartUI()
    {
        StartCoroutine(DelaySetActive(true));
        if (elements.Count != 0 && elementIndex == -1)
        {
            curUIElement = elements[0];
            curUIElement.StartUI();
            elementIndex = 0;
        }
        if (buttons.Count != 0 && buttonIndex == -1)
        {
            curButton = buttons[0];
            curButton.Choose();
            buttonIndex = 0;
        }
    }
    public virtual void EndUI()
    {
        isActive = false;
    }
    public IEnumerator DelaySetActive(bool active)
    {
        yield return new WaitForEndOfFrame();
        isActive = active;
    }
}
