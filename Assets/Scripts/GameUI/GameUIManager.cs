using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private List<Panel> panels;
    [SerializeField] private Panel startPanel;
    private Panel curPanel;
    public virtual void Start()
    {
        curPanel = startPanel;
        curPanel.StartPanel();
    }
    public virtual void ChangePanel(Panel panel)
    {
        curPanel.EndPanel();
        curPanel = panel;
        curPanel.StartPanel();
    }
}
