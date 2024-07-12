using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TrampolineProjectile_ThrownState : IThrowObjectState
{
    TrampolineProjectileController controller;

    GameObject controllerObj;

    public TrampolineProjectile_ThrownState(TrampolineProjectileController controller)
    {
        this.controller = controller;
    }

    public void OnBeginState()
    {
        //allow the object to move
        controllerObj = controller.gameObject;
        controllerObj.transform.eulerAngles = controller.Speed > 0 ? new Vector3(controllerObj.transform.eulerAngles.x, controllerObj.transform.eulerAngles.y, controller.AngleRight) : new Vector3(controllerObj.transform.eulerAngles.x, controllerObj.transform.eulerAngles.y, controller.AngleLeft);
        controller.StartMovement = true;
    }

    public void OnUpdate()
    {
        //move the object
        if (!controller.StartMovement) return;

        controller.rb.velocity = new Vector2(controller.Speed, controller.rb.velocity.y);

        if (!controller.Player) return;

        float currentDistanceToPlayer = Vector3.Distance(controller.Player.transform.position, controllerObj.transform.position);

        if (currentDistanceToPlayer > controller.MaxDistanceToPlayer) { controller.Die(); }
    }

    public IThrowObjectState ChangeState()
    {
        //stop the object from moving
        throw new System.NotImplementedException();
    }


}
