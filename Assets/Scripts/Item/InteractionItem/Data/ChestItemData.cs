using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Chest Item Data")]
public class ChestItemData : ItemData
{
    public List<ItemInChess> itemInChessList;
}
[System.Serializable]
public class ItemInChess
{
    public GameObject itemPrefab;
    public int count;
}
