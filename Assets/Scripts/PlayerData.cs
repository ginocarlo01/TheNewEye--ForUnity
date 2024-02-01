using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Vector3 lastCheckPoint;
    

    [System.Serializable]
    public class CollectableData
    {
        public CollectableNames collectableName;
        public int quantity;

    }

    [System.Serializable]
    public struct ListOfLevels
    {
        public Vector3 lastLocation;
        public Dictionary<CollectableNames, int> collectableQty;
        public bool levelCompleted;
    }

    [SerializeField] public ListOfLevels[] arrayOfLevels;

    public void ChangeBoolAtIndex(int index, bool newBool)
    {
        if (index >= 0 && index < arrayOfLevels.Length)
        {
            ListOfLevels data = arrayOfLevels[index];
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

        for (int i = 0; i < arrayOfLevels.Length; i++)
        {
            ListOfLevels newData = new ListOfLevels
            {
                lastLocation = Vector3.zero,
                //fruitsQty = 0,
                levelCompleted = false
            };

            arrayOfLevels[i] = newData;
        }
    }

    public void AddNewLevelData()
    {
        int currentLength = arrayOfLevels.Length;

        ListOfLevels[] newArray = new ListOfLevels[currentLength + 1];
        for (int i = 0; i < currentLength; i++)
        {
            newArray[i] = arrayOfLevels[i];
        }

        newArray[currentLength] = new ListOfLevels
        {
            lastLocation = Vector3.zero,
            //fruitsQty = 0,
            levelCompleted = false
        };

        arrayOfLevels = newArray;
    }
}
