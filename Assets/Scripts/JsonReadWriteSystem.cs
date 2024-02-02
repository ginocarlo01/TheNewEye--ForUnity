using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

public class JsonReadWriteSystem : MonoBehaviour
{
    [SerializeField] private string fileName;

    public PlayerData playerData;

    public static JsonReadWriteSystem INSTANCE;

    public int currentLvlIndex;

    public int qtyOfLevels = 6;

    public static Action<List<PlayerData.CollectableData>> CollectableDataLoadedAction;

    private void Awake()
    {
        if (INSTANCE)
        {
            Destroy(gameObject);
            return;
        }

        INSTANCE = this;


        Load();


        if(playerData.collectableDataList.Count == 0)
        {
            playerData.AddNewCollectableData();
        }

        CollectableDataLoadedAction?.Invoke(playerData.collectableDataList);

        if(playerData.arrayOfLevels.Count > qtyOfLevels )
        {
            for(int i = playerData.arrayOfLevels.Count; i > qtyOfLevels; i++)
            {
               playerData.arrayOfLevels.RemoveAt(i);
            }
        }

        else
        {
            while(playerData.arrayOfLevels.Count < qtyOfLevels)
            {
                playerData.AddNewLevelData();
            }
        }

    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ResetData();
        }


    }

    private void WriteToFile(string filename, string jsonData)
    {
        string path = GetFilePath(filename);

        FileStream fileStream = new FileStream(path, FileMode.Create);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(jsonData);
        }
    }

    private string ReadFromFile(string filename)
    {
        string path = GetFilePath(filename);

        if (!File.Exists(path))
        {
            Debug.Log("File does not exist!");
            return "";
        }

        using (StreamReader reader = new StreamReader(path))
        {
            return reader.ReadToEnd();
        }
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(playerData);

        WriteToFile(fileName, json);

    }

    public void Load()
    {
        Debug.Log("Load done");
        string json = ReadFromFile(fileName);
        Debug.Log(fileName + " path " + GetFilePath(fileName));
        JsonUtility.FromJsonOverwrite(json, playerData);

    }

    public void ResetData()
    {
        playerData.ResetData();
        Save();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    private void OnDestroy()
    {
        Save();
    }

    private void UpdateLastPosition(Vector3 newPosition)
    {
        PlayerData.LevelData new_ = playerData.arrayOfLevels[currentLvlIndex];
        new_.lastLocation = newPosition;
        playerData.arrayOfLevels[currentLvlIndex] = new_;

    }

    private void CompleteLevel()
    {
        playerData.ResetCheckPoint(currentLvlIndex);
        playerData.ChangeBoolAtIndex(currentLvlIndex, true);
    }

    private void UpdateCollectableData(Dictionary<CollectableNames, int> collectableQty_)
    {
        playerData.SaveCollectableData(collectableQty_);
    }

    private void UpdateTimerData(float time)
    {
        playerData.SaveTimerData(currentLvlIndex,time);
    }

    public float GetCurrentLevelTime()
    {
        return playerData.arrayOfLevels[currentLvlIndex].timer;
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        CheckPoint.saveCheckPointAction += UpdateLastPosition;
        LoadNextLevel.finishLevelAction += CompleteLevel;
        PlayerMovement.updateSpawnAction += UpdateLastPosition;
        CollectableManager.saveDataAction += UpdateCollectableData;
        TimerManager.saveTimeDataAction += UpdateTimerData;
    }

    private void OnDisable()
    {
        CheckPoint.saveCheckPointAction -= UpdateLastPosition;
        LoadNextLevel.finishLevelAction -= CompleteLevel;
        PlayerMovement.updateSpawnAction -= UpdateLastPosition;
        CollectableManager.saveDataAction -= UpdateCollectableData;
        TimerManager.saveTimeDataAction -= UpdateTimerData;
    }
    #endregion
}