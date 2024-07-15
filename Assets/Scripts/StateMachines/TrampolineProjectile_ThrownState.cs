using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TrampolineProjectile_ThrownState : IThrowObjectState
{

    IThrowObjectState nextState;

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
        nextState = controller.wallState;

        controller.SurfaceBlocker.SetActive(false);

        controller.IsPointingToRightDirection = controller.Speed > 0;
        controllerObj.transform.eulerAngles = controller.IsPointingToRightDirection ? new Vector3(controllerObj.transform.eulerAngles.x, controllerObj.transform.eulerAngles.y, controller.AngleRight) : new Vector3(controllerObj.transform.eulerAngles.x, controllerObj.transform.eulerAngles.y, controller.AngleLeft);
        controller.SurfaceBlocker.transform.localEulerAngles = controller.IsPointingToRightDirection ? new Vector3(controller.SurfaceBlocker.transform.eulerAngles.x, controller.SurfaceBlocker.transform.eulerAngles.y, controllerObj.transform.eulerAngles.z + 90) : new Vector3(controller.SurfaceBlocker.transform.eulerAngles.x, controller.SurfaceBlocker.transform.eulerAngles.y, controllerObj.transform.eulerAngles.z + 90);

        controller.rb.velocity = new Vector2(controller.Speed, controller.rb.velocity.y);
    }

    public void OnUpdate()
    {

        if (!controller.Player) return;

        float currentDistanceToPlayer = Vector3.Distance(controller.Player.transform.position, controllerObj.transform.position);

        if (currentDistanceToPlayer > controller.MaxDistanceToPlayer) { controller.CallProjectileBack(); }
    }

    public IThrowObjectState ChangeState()
    {
        StopMovement();
        return nextState;
    }

    private void StopMovement()
    {
        if(!controller.rb.bodyType.Equals(RigidbodyType2D.Static)) controller.rb.velocity = new Vector2(0, controller.rb.velocity.y);
        controller.rb.bodyType = RigidbodyType2D.Static;
    }

}
