using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Button : MonoBehaviour
{
    protected Image buttonImage;
    public virtual void Start()
    {
        buttonImage = GetComponent<Image>();
    }
    public virtual void Choose()
    {

    }
    public virtual void UnChoose()
    {

    }
    public virtual void TriggerButton()
    {
        Debug.Log(gameObject.name);
    }
}
