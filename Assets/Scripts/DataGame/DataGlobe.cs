using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
// TODO: fix enemy, 
public class DataGlobe : MonoBehaviour
{
    [SerializeField] public GameObject player;/* { get; private set; }*/ //TODO: Test
    public int saveIndex;
    public GameObject playerPrefab;
    public SystemUI systemUI;
    public static DataGlobe instance;
    public Canvas systemCanvas;


    public PlayerAttributeData playerAttributeData;
    public Inventory abilityInventory;
    public Inventory consumableInventory;
    [Header("RuneConifg")]
    public List<RuneTableStatus> runeTableStatusList;
    public List<RuneTable> runeTableList;

    public PlayerInputHandler inputHandler { get; private set; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            inputHandler = GetComponent<PlayerInputHandler>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        //TODO: Test
        //systemUI.UpdateAttributeUI();
    }
    private void Update()
    {
    }
    public void SaveCurData()
    {
        if (!Directory.Exists(string.Concat(Application.persistentDataPath, "/Save", saveIndex))) 
        {
            Directory.CreateDirectory(string.Concat(Application.persistentDataPath, "/Save", saveIndex));
        }
        if (player != null)
        {
            playerAttributeData.playerSavePosition = player.transform.position;
        }
        playerAttributeData.Save(saveIndex);
        abilityInventory.Save(saveIndex, "Ability");
        consumableInventory.Save(saveIndex, "Consumable");
    }
    public void DeleteData(int saveIndex)
    {
        playerAttributeData.Delete(saveIndex);
        abilityInventory.Delete(saveIndex, "Ability");
        consumableInventory.Delete(saveIndex, "Consumable");
    }
    public void LoadData(int saveIndex)
    {
        this.saveIndex = saveIndex;
        playerAttributeData.Load(saveIndex);
        abilityInventory.Load(saveIndex, "Ability");
        consumableInventory.Load(saveIndex, "Consumable");
    }
    public void CreateNewData(int saveIndex)
    {
        this.saveIndex = saveIndex;
        playerAttributeData.CrearteNew();
        abilityInventory.CreateNewData();
        consumableInventory.CreateNewData();
    }
    public void PickItem(ItemData itemData, int count)
    {
        if (itemData.type == ItemType.Ability)
        {
            abilityInventory.AddItem(itemData, 1);
        }
        else if (itemData.type == ItemType.Consumable)
        {
            consumableInventory.AddItem(itemData, count);
        }
        else if (itemData.type == ItemType.Gold)
        {
            playerAttributeData.curGold += count;
        }
        else if (itemData.type == ItemType.Soul)
        {
            playerAttributeData.maxSoul += count;
            playerAttributeData.curSoul += count;
        }
        systemUI.PickUp(itemData, count);
    }
    public void LoadScene(string sceneName, bool createPlayer)
    {
        StartCoroutine(LoadSceneAsync(sceneName, createPlayer));
    }
    IEnumerator LoadSceneAsync(string sceneName, bool createPlayer)
    {
        systemUI.StartLoadScene();
        yield return new WaitForSecondsRealtime(1.5f);
        AsyncOperation asyncOperation =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        while (!asyncOperation.isDone)
        {
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSecondsRealtime(1.5f);
        systemCanvas.worldCamera = Camera.main;
        systemUI.StopLoadScene();
        if (createPlayer)
        {
            player = Instantiate(playerPrefab, playerAttributeData.playerSavePosition, Quaternion.identity);
            inputHandler.ChangeActionMap("GamePlay");
            systemUI.UpdateAttributeUI();
            systemUI.UpdateRuneTable();
        }
    }
    public void ResetRune()
    {
        for (int i = 0; i < playerAttributeData.runeTableStatusList.Count; i++)
        {
            for (int j = 0; j < playerAttributeData.runeTableStatusList[i].tierTableStatusList.Count; j++)
            {
                for (int k = 0; k < playerAttributeData.runeTableStatusList[i].tierTableStatusList[j].runeStatusesList.Count; k++)
                {
                    if (playerAttributeData.runeTableStatusList[i].tierTableStatusList[j].runeStatusesList[k] == RuneStatus.Active)
                    {
                        playerAttributeData.runeTableStatusList[i].tierTableStatusList[j].runeStatusesList[k] = RuneStatus.Deactive;
                    }
                }
            }
        }
        
        playerAttributeData.curSoul = playerAttributeData.maxSoul;
        systemUI.UpdateRuneTable();
        systemUI.UpdateAttributeUI();
    }
    public RuneStatus GetRuneStatus(int table, int tier, int index)
    {
        return playerAttributeData.runeTableStatusList[table].tierTableStatusList[tier].runeStatusesList[index];
    }
    public void SetRuneStatus(int table, int tier, int index, RuneStatus runeStatus)
    {
        playerAttributeData.runeTableStatusList[table].tierTableStatusList[tier].runeStatusesList[index] = runeStatus;
    }
 #region DebugFunction
    public void LockTier(int tier)
    {
        for (int i = 0; i < playerAttributeData.runeTableStatusList.Count; i++)
        {
            for (int k = 0; k < playerAttributeData.runeTableStatusList[i].tierTableStatusList[tier].runeStatusesList.Count; k++)
            {
                if (playerAttributeData.runeTableStatusList[i].tierTableStatusList[tier].runeStatusesList[k] != RuneStatus.Hidden)
                {
                    playerAttributeData.runeTableStatusList[i].tierTableStatusList[tier].runeStatusesList[k] = RuneStatus.Lock;
                }
            }
        }
        systemUI.UpdateRuneTable();
    }
    public void UnlockTier(int tier)
    {
        for (int i = 0; i < playerAttributeData.runeTableStatusList.Count; i++)
        {
            for (int k = 0; k < playerAttributeData.runeTableStatusList[i].tierTableStatusList[tier].runeStatusesList.Count; k++)
            {
                if (playerAttributeData.runeTableStatusList[i].tierTableStatusList[tier].runeStatusesList[k] != RuneStatus.Hidden)
                {
                    playerAttributeData.runeTableStatusList[i].tierTableStatusList[tier].runeStatusesList[k] = RuneStatus.Deactive;
                }
            }
        }
        systemUI.UpdateRuneTable();
    }
    #endregion
}
