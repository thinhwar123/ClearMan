using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemData : ScriptableObject
{
    public ItemType type;
    public string itemName;
    public Sprite icon;
    public string description;
}
[System.Serializable]
public enum ItemType
{
    Ability,
    Consumable,
    Runes,
    Gold,
    Soul,
    Other,
}
