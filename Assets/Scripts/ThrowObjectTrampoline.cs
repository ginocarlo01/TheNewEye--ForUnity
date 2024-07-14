using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectTrampoline : Trampoline
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CollideWithPlayer(collision);
            TriggerAnim();
            ChangeParentState();
        }
    }

    private void ChangeParentState()
    {
        GetComponentInParent<TrampolineProjectileController>().ChangeState();
    }

    public void TriggerAnim()
    {
        Debug.Log("Triggered animation");
        if (!animator) { Debug.Log("There is no animator");  }
         animator.SetTrigger("collideToPlayer");
    }
}
