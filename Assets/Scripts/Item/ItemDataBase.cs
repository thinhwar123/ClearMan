using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newItemDatabase", menuName = "Data/Inventory Data/Item Database")]
public class ItemDataBase : ScriptableObject, ISerializationCallbackReceiver
{
    public List<ItemData> itemList;
    public Dictionary<ItemData, int> getID;
    public Dictionary<int, ItemData> getItem;
    public void OnAfterDeserialize()
    {
        getID = new Dictionary<ItemData, int>();
        getItem = new Dictionary<int, ItemData>();

        for (int i = 0; i < itemList.Count; i++)
        {
            getID.Add(itemList[i], i);
            getItem.Add(i, itemList[i]);
        }
    }

    public void OnBeforeSerialize()
    {
        
    }
}
