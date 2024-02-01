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

    private void Start()
    {
        //JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].fruitsQty;
        //TODO: FIX THE LINE ABOVE
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        collectableQty = new Dictionary<CollectableNames, int>();

        foreach (var collectableData in collectableDataArray)
        {
            collectableQty.Add(collectableData.collectableName, collectableData.quantity);
        }
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        Collectable.collectedAction += UpdateCollectableQty;
        PlayerLife.playerDeath += CleanCollectableData;
    }

    private void OnDisable()
    {
        Collectable.collectedAction -= UpdateCollectableQty;
        PlayerLife.playerDeath -= CleanCollectableData;
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

        //JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].fruitsQty = 0;
        
        //todo: fix the json instance above
    }

    
}
