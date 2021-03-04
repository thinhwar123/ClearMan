using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
[CreateAssetMenu(fileName = "newInventoryData", menuName = "Data/Inventory Data/InventoryData")]
public class Inventory : ScriptableObject,  ISerializationCallbackReceiver
{
    private ItemDataBase itemDataBase;
    public List<InventoryItemSlot> itemList;
    private void OnEnable()
    {
#if UNITY_EDITOR
        itemDataBase = (ItemDataBase)AssetDatabase.LoadAssetAtPath("Assets/Resources/DatabaseItem/ItemDatabase.Asset", typeof(ItemDataBase));
#else
        itemDataBase = Resources.Load<ItemDataBase>("DatabaseItem");
#endif
    }
    public void OnAfterDeserialize()
    {
        if (itemDataBase!= null)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                itemList[i].itemData = itemDataBase.getItem[itemList[i].id];
            }
        }

    }
    public void AddItem(ItemData itemData, int count)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].itemData == itemData)
            {
                itemList[i].count += count;
                return;
            }
        }
        itemList.Add(new InventoryItemSlot(itemDataBase.getID[itemData], itemData, count));
    }
    public void Save(int saveIndex, string saveName)
    {
        //Debug.Log(string.Concat(Application.persistentDataPath, "/Save", saveIndex, "/Inventory", saveName));
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, "/Save",saveIndex, "/Inventory", saveName));
        bf.Serialize(file, saveData);
        file.Close();
    }
    public void Load(int saveIndex, string saveName)
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, "/Save", saveIndex, "/Inventory", saveName)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, "/Save",saveIndex,"/Inventory", saveName), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
    public void Delete(int saveIndex, string saveName)
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, "/Save", saveIndex, "/Inventory", saveName)))
        {
            File.Delete(string.Concat(Application.persistentDataPath, "/Save", saveIndex, "/Inventory", saveName));
        }
    }
    public void OnBeforeSerialize()
    {

    }
    public void CreateNewData()
    {
        itemList = new List<InventoryItemSlot>();
    }
}
[System.Serializable]
public class InventoryItemSlot
{
    public int id;
    public ItemData itemData;
    public int count;

    public InventoryItemSlot(int id, ItemData itemData, int count)
    {
        this.id = id;
        this.itemData = itemData;
        this.count = count;
    }
}