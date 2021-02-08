using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataManager 
{
    public static SaveData ReadData(int slotIndex)
    {
        string path = "Assets/Resources/GameJSONData/SaveGame" + slotIndex + ".json";
        if (File.Exists(path))
        {            
            using(StreamReader reader = new StreamReader(path))
            {
                SaveData saveData = new SaveData();
                string json = reader.ReadToEnd();
                JsonUtility.FromJsonOverwrite(json, saveData);
                return saveData;
            }            
        }
        return null;
    }
    public static void WriteData(SaveData saveData, int slotIndex)
    {
        string json = JsonUtility.ToJson(saveData);
        FileStream fs = new FileStream("Assets/Resources/GameJSONData/SaveGame" + slotIndex + ".json", FileMode.OpenOrCreate);
        using(StreamWriter writer = new StreamWriter(fs))
        {
            writer.Write(json);
        }
    }
    public static void DeleteData(int slotIndex)
    {
        string path = "Assets/Resources/GameJSONData/SaveGame" + slotIndex + ".json";
        File.Delete(path);
    }
    
}
