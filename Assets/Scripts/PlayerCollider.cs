using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField] private int cherries = 0;

    [SerializeField] private AudioSource collectItemSFX;

    private PlayerMovement pm;

    [SerializeField] private bool floatingUp, floatingDown;

    private void Start()
    {
        
        cherries = JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].fruitsQty;
        UIManager.instance.UpdateCherryCount(cherries);
        pm = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cherry")
        {
            collectItemSFX.Play();
            Destroy(collision.gameObject);
            cherries++;
            UIManager.instance.UpdateCherryCount(cherries);
            JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].fruitsQty = cherries;
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
}
