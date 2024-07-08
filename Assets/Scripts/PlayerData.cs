using System;
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
        public float timer;
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
            collectableDataList.Add(new CollectableData
            {
                collectableName = key,
                quantity = collectableQty_[key]
            });
            i++;
        }
    }


    public void SaveTimerData(int index, float time)
    {
        if (index >= 0 && index < arrayOfLevels.Count)
        {
            LevelData data = arrayOfLevels[index];
            data.timer = time;
            arrayOfLevels[index] = data;
        }
        else
        {
            Debug.LogError("Invalid index.");
        }

        Debug.Log("Timer at level " + index + " is " + arrayOfLevels[index].timer);
    }

    public void ResetCheckPoint(int index)
    {
        if (index >= 0 && index < arrayOfLevels.Count)
        {
            LevelData data = arrayOfLevels[index];
            data.lastLocation = Vector3.zero;
            arrayOfLevels[index] = data;
        }
        else
        {
            Debug.LogError("Invalid index.");
        }
    }

    public void ChangeLevelCompletedAtIndex(int index, bool newBool)
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
        arrayOfLevels.Clear();

        collectableDataList.Clear();
    }

    public void AddNewLevelData()
    {
        arrayOfLevels.Add(new LevelData
        {
            lastLocation = Vector3.zero,
            timer = 0,
            levelCompleted = false
        }); ;
    }

    public void AddNewCollectableData()
    {
        collectableDataList.Add(new CollectableData
        {
            collectableName = CollectableNames.RedCherry,
            quantity = 0
        }) ;
    }

    public void MoveLevelData(int currentIndex, int newIndex)
    {
        if(currentIndex >= 0 && currentIndex < arrayOfLevels.Count && newIndex >= 0 && newIndex < arrayOfLevels.Count)
        {
            LevelData data = arrayOfLevels[currentIndex];

            arrayOfLevels[newIndex] = data;

            data.timer = 0;
            data.lastLocation = Vector3.zero;
            data.levelCompleted = false;

            arrayOfLevels[currentIndex] = data;
        }
        else
        {
            Debug.Log("Wrong index");
        }
    }

    public void RemoveLevelData(int index)
    {
        if(index >= 0 && index < arrayOfLevels.Count)
        {
            LevelData data = arrayOfLevels[index];

            data.timer = 0;
            data.lastLocation = Vector3.zero;
            data.levelCompleted = false;

            arrayOfLevels[index] = data;
        }
    }

    public void CompareLevelData(int original, int comp)
    {
        if (original >= 0 && original < arrayOfLevels.Count && comp >= 0 && comp < arrayOfLevels.Count)
        {

            if (arrayOfLevels[original].timer > arrayOfLevels[comp].timer && arrayOfLevels[comp].levelCompleted)
            {
                LevelData data = arrayOfLevels[original];

                data.timer = arrayOfLevels[comp].timer;

                arrayOfLevels[original] = data;

                RemoveLevelData(comp);

            }
        }
        else
        {
            Debug.Log("Wrong index");
        }
    }

}
