using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newTableRuneData", menuName = "Data/Item Data/Rune Table Data")]
public class RuneTable : ScriptableObject
{
    public List<Tier> tierList;
}
[System.Serializable] public class Tier
{
    public List<RuneItemData> runeItemDataList;
}