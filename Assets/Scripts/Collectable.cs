using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Collectable : MonoBehaviour
{
    public static Action<SFX> collectedActionSFX;

    public static Action<CollectableNames, int> collectedAction;

    [SerializeField] private int qty = 1;

    [SerializeField] private CollectableNames collectableName;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            collectedAction?.Invoke(collectableName, qty);
            collectedActionSFX?.Invoke(SFX.CollectItem);
            //JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].fruitsQty = qty; //TODO: there is no need to update this every time we collect one, we just need to do this once we ie
            //TODO: FIX THE LINE ABOVE!

            this.gameObject.SetActive(false);
        }
    }
}
