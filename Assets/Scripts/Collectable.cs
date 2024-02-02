using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Collectable : MonoBehaviour
{
    public static Action<SFX> collectedActionSFX;
    public static Action<CollectableNames, int> collectedAction;

    [SerializeField] protected int qty = 1;

    [SerializeField] protected CollectableNames collectableName;

    [SerializeField] protected SFX collectedSFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
            collectedAction?.Invoke(collectableName, qty);
            collectedActionSFX?.Invoke(collectedSFX);

            this.gameObject.SetActive(false);
        }
    }
}
