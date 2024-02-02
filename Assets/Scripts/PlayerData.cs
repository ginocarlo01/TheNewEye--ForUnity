using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Vector3 defaultData; //this data needs to be here in order to work
    

    [System.Serializable]
    public struct CollectableData
    {
        public CollectableNames collectableName;
        public int quantity;
    }

    [System.Serializable]
    public struct LevelData
    {
        public Vector3 lastLocation;
        public bool levelCompleted;
    }


    [SerializeField] public List<LevelData> arrayOfLevels;

    public List<CollectableData> collectableDataList;

    private void Awake()
    {
        collectableDataList = new List<CollectableData>();
    }


    public void SaveCollectableData(Dictionary<CollectableNames, int> collectableQty_)
    {
        collectableDataList.Clear(); // Clear the existing list

        List<CollectableNames> keysToRemove = new List<CollectableNames>(collectableQty_.Keys);

        int i = 0;
        foreach (CollectableNames key in keysToRemove)
        {
            Debug.Log(key + ": " + collectableQty_[key]);
            collectableDataList.Add(new CollectableData
            {
                collectableName = key,
                quantity = collectableQty_[key]
            });
            i++;
        }
    }

    public void PrintCollectableDataList()
    {
        Debug.Log("Collectable Data List:");

        foreach (CollectableData data in collectableDataList)
        {
            Debug.Log("Collectable Name: " + data.collectableName + ", Quantity: " + data.quantity);
        }
    }



    public void ChangeBoolAtIndex(int index, bool newBool)
    {
        if (index >= 0 && index < arrayOfLevels.Count)
        {
            LevelData data = arrayOfLevels[index];
            data.levelCompleted = newBool;
            arrayOfLevels[index] = data;
        }
        else
        {
            Debug.LogError("Invalid index.");
        }
    }

    public void ResetData()
    {
        for (int i = 0; i < arrayOfLevels.Count; i++)
        {
            LevelData newData = new LevelData
            {
                lastLocation = Vector3.zero,
                levelCompleted = false
            };

            arrayOfLevels[i] = newData;
        }
    }

    public void AddNewLevelData()
    {
        arrayOfLevels.Add(new LevelData
        {
            lastLocation = Vector3.zero,
            levelCompleted = false
        });
    }

    public void AddNewCollectableData()
    {
        collectableDataList.Add(new CollectableData
        {
            collectableName = CollectableNames.RedCherry,
            quantity = 0
        }) ;
    }

}
