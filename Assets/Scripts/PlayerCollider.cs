using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private int cherriesQty = 0;

    [SerializeField] private AudioSource collectItemSFX;

    private PlayerMovement pm;

    [SerializeField] private bool floatingUp, floatingDown;

    public static Action<int> cherriesQtyChanged;

    private void Start()
    {
        
        cherriesQty = JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].fruitsQty;
        //UIManager.instance.UpdateCherryCount(cherriesQty); //TODO: APLICAR OBSERVER
        cherriesQtyChanged?.Invoke(cherriesQty);
        pm = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cherry")
        {
            collectItemSFX.Play();
            collision.gameObject.SetActive(false);
            cherriesQty++;
            cherriesQtyChanged?.Invoke(cherriesQty);
            JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].fruitsQty = cherriesQty;
        }

        if (collision.gameObject.tag == "Up")
        {
            collectItemSFX.Play();
            floatingUp = !floatingUp;
            pm.SetFloatingUp(floatingUp);
        }

        if (collision.gameObject.tag == "Down")
        {
            collectItemSFX.Play();
            floatingDown = !floatingDown;
            pm.SetFloatingDown(floatingDown);
        }
    }

    public void ResetCherries()
    {
        cherriesQty=0;
        cherriesQtyChanged?.Invoke(cherriesQty);
        JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].fruitsQty = cherriesQty;
    }

    #region ObserverSubscription
    private void OnEnable()
    {
        PlayerLife.playerDeath += ResetCherries;
    }

    private void OnDisable()
    {
        PlayerLife.playerDeath -= ResetCherries;
    }
    #endregion
}
