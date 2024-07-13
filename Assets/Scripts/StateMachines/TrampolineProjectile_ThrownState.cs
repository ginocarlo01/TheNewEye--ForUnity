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
        controller.IsPointingToRightDirection = controller.Speed > 0;
        controllerObj.transform.eulerAngles = controller.IsPointingToRightDirection? new Vector3(controllerObj.transform.eulerAngles.x, controllerObj.transform.eulerAngles.y, controller.AngleRight) : new Vector3(controllerObj.transform.eulerAngles.x, controllerObj.transform.eulerAngles.y, controller.AngleLeft);
        controller.rb.velocity = new Vector2(controller.Speed, controller.rb.velocity.y);
    }

    public void OnUpdate()
    {

        if (!controller.Player) return;

        float currentDistanceToPlayer = Vector3.Distance(controller.Player.transform.position, controllerObj.transform.position);

        if (currentDistanceToPlayer > controller.MaxDistanceToPlayer) { controller.Die(); }
    }

    public IThrowObjectState ChangeState()
    {
        //stop the object from moving 
        controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
        controller.rb.bodyType = RigidbodyType2D.Static;
        return controller.wallState;
    }


}
