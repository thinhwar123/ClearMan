using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerAttributeData", menuName = "Data/Player Data/PlayerAttributeData")]
public class PlayerAttributeData : ScriptableObject
{
    public float curHitPoint;
    public float curManaPoint;
    public float curGold;
    public float curSoul;
    public float curAttackDamage;

    public float maxHitPoint;
    public float maxManaPoint;
    public float maxSoul;
    public float maxAttackDamage;

    public string curMap;
    public int curHours;
    public int curMinutes;

    public Vector3 playerSavePosition;
    public List<RuneTableStatus> runeTableStatusList;

    public void Save(int saveIndex)
    {
        //Debug.Log(string.Concat(Application.persistentDataPath, "/SaveInventory", saveName));

        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, "/Save", saveIndex,"/PlayerAttribute"));
        bf.Serialize(file, saveData);
        file.Close();
    }
    public bool Load(int saveIndex)
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, "/Save", saveIndex, "/PlayerAttribute")))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, "/Save", saveIndex, "/PlayerAttribute"), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
            return true;
        }
        return false;
    }
    public void Delete(int saveIndex)
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, "/Save", saveIndex, "/PlayerAttribute")))
        {
            File.Delete(string.Concat(Application.persistentDataPath, "/Save", saveIndex, "/PlayerAttribute"));
        }
    }
    public void CrearteNew()
    {
        this.curHitPoint = 50;
        this.curManaPoint = 50;
        this.curGold = 0;
        this.curSoul = 0;
        this.curAttackDamage = 1;

        this.maxHitPoint = 50;
        this.maxManaPoint = 50;
        this.maxSoul = 0;
        this.maxAttackDamage = 10;

        this.curMap = "StartMap";
        this.curHours = 0;
        this.curMinutes = 0;

        this.playerSavePosition = Vector3.zero;

        runeTableStatusList = DataGlobe.instance.runeTableStatusList;
    }
}
//TODO: gia tri khoi dau cua bang rune
[System.Serializable]
public class RuneTableStatus
{
    public List<TierTableStatus> tierTableStatusList;
}
[System.Serializable]
public class TierTableStatus
{
    public List<RuneStatus> runeStatusesList;
}