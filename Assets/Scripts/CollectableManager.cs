using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CollectableData
{
    public CollectableNames collectableName;
    public int quantity;

}
public class CollectableManager : MonoBehaviour
{
    public CollectableData[] collectableDataArray;

    public Dictionary<CollectableNames, int> collectableQty;

    public static Action<CollectableNames, int> updateUICollectable;

    public static Action<Dictionary<CollectableNames, int>> saveDataAction;

    private void Start()
    {
        
        InitalizeList(JsonReadWriteSystem.INSTANCE.playerData.collectableDataList);
    }

    private void InitializeDictionary()
    {
        collectableQty = new Dictionary<CollectableNames, int>();

        foreach (var collectableData in collectableDataArray)
        {
            collectableQty.Add(collectableData.collectableName, collectableData.quantity);
            updateUICollectable?.Invoke(collectableData.collectableName, collectableData.quantity);
        }
    }

    private void InitalizeList(List<PlayerData.CollectableData> collectableDatas)
    {
        collectableDataArray = new CollectableData[collectableDatas.Count];

        for (int i = 0; i < collectableDatas.Count; i++)
        {
            collectableDataArray[i] = new CollectableData
            {
                collectableName = collectableDatas[i].collectableName,
                quantity = collectableDatas[i].quantity
            };
        }

        InitializeDictionary();
    }



    #region ObserverSubscription
    private void OnEnable()
    {
        Collectable.collectedAction += UpdateCollectableQty;
        PlayerLife.playerDeath += CleanCollectableData;
        CheckPoint.saveCheckPointAction += SaveDataCheckPoint;
        LoadNextLevel.finishLevelAction += SaveDataCheckPoint;
    }

    private void OnDisable()
    {
        Collectable.collectedAction -= UpdateCollectableQty;
        PlayerLife.playerDeath -= CleanCollectableData;
        CheckPoint.saveCheckPointAction -= SaveDataCheckPoint;
        LoadNextLevel.finishLevelAction -= SaveDataCheckPoint;
    }
    #endregion

    private void UpdateCollectableQty(CollectableNames collectableNames, int qty)
    {
        if(collectableQty.ContainsKey(collectableNames))
        {
            collectableQty[collectableNames] += qty;
            updateUICollectable?.Invoke(collectableNames, collectableQty[collectableNames]);
        }
    }

    private void CleanCollectableData()
    {
        List<CollectableNames> keysToRemove = new List<CollectableNames>(collectableQty.Keys);

        foreach (CollectableNames key in keysToRemove)
        {
            collectableQty[key] = 0;
        }

    }

    public void SaveDataCheckPoint(Vector3 uselessData)
    {
        saveDataAction?.Invoke(collectableQty);
    }

    public void SaveDataCheckPoint()
    {
        saveDataAction?.Invoke(collectableQty);
    }
}
