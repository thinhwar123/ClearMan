using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Rune Item Data")]
public class RuneItemData : ItemData
{
    public int table;
    public int tier;
    public int index;
    public float soulRequire;
    public List<RuneRequire> runeRequireList;
}

[System.Serializable]
public class RuneRequire
{
    public RuneItemData runeRequireData;
    public Direction runeRequireOffsetPosition;
}
[System.Serializable]
public enum RuneStatus
{
    Lock,
    Deactive,
    Active,
    Hidden,
}
[System.Serializable]
public enum Direction
{
    Up,
    Left,
    Right,
    Down,
}