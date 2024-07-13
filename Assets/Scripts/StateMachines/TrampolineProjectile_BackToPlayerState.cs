using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TrampolineProjectile_BackToPlayerState : IThrowObjectState
{

    TrampolineProjectileController controller;

    GameObject controllerObj;

    public TrampolineProjectile_BackToPlayerState(TrampolineProjectileController controller)
    {
        this.controller = controller;
    }

    public IThrowObjectState ChangeState()
    {
        throw new System.NotImplementedException();
    }

    public void OnBeginState()
    {
        //calculate the distance to the player and based on this information decide if its going to be destroyed or not
        controllerObj = controller.gameObject;
        Debug.Log("Começou o back to player");
        
    }

    public void OnUpdate()
    {
        if (!controller.Player || !controllerObj) return;

        controllerObj.transform.position = Vector3.MoveTowards(controllerObj.transform.position, controller.Player.transform.position, Mathf.Abs(controller.Speed)*Time.deltaTime);

        float currentDistanceToPlayer = Vector3.Distance(controller.Player.transform.position, controllerObj.transform.position);

        if (currentDistanceToPlayer > controller.MaxDistanceToPlayer*2) { Debug.Log(currentDistanceToPlayer);  controller.Die(); }

        if (currentDistanceToPlayer < controller.MinDistanceToPlayer) { Debug.Log(currentDistanceToPlayer); controller.BackToPlayer(); }
    }
}
