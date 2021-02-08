using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData 
{
    public float curHealth;
    public float curMana;
    public float curGold;
    public float curSoul;
    public string curMap;
    public int curHours;
    public int curMinutes;

    public SaveData(float curHealth, float curMana, float curGold, float curSoul, string curMap, int curHours, int curMinutes)
    {
        this.curHealth = curHealth;
        this.curMana = curMana;
        this.curGold = curGold;
        this.curSoul = curSoul;
        this.curMap = curMap;
        this.curHours = curHours;
        this.curMinutes = curMinutes;
    }
    public SaveData()
    {
        this.curHealth = 10;
        this.curMana = 10;
        this.curGold = 0;
        this.curSoul = 0;
        this.curMap = "StartMap";
        this.curHours = 0;
        this.curMinutes = 0;
    }
}
