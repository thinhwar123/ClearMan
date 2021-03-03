using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class testUI : Selectable
{
    public bool isInteractable;
    // Update is called once per frame
    void Update()
    {
        isInteractable = IsInteractable();
    }
}
