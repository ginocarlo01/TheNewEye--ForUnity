using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineProjectile_OnWallState : IThrowObjectState
{
    TrampolineProjectileController controller;

    GameObject controllerObj;

    public TrampolineProjectile_OnWallState(TrampolineProjectileController controller) 
    { 
        this.controller = controller;
    }


    public IThrowObjectState ChangeState()
    {
        DisableNewColliders();
        return controller.backToPlayerState;
    }

    public void OnBeginState()
    {
        controllerObj = controller.gameObject;
        DisableOldColliders();
        EnableNewColliders();
    }

    public void OnUpdate()
    {
        if (!controller.Player) return;

        float currentDistanceToPlayer = Vector3.Distance(controller.Player.transform.position, controllerObj.transform.position);

        if (currentDistanceToPlayer > controller.MaxDistanceToPlayer) { controller.ChangeState(); }
    }

    private void DisableOldColliders()
    {
        BoxCollider2D collider = controllerObj.GetComponent<BoxCollider2D>();

        collider.enabled = false;

       
    }

    private void EnableNewColliders()
    {
        //make one way collision active

        controller.SurfaceCollider.SetActive(true);
        controller.Blockers.SetActive(true);

        //change the collision rotation

        controller.SurfaceCollider.GetComponent<PlatformEffector2D>().rotationalOffset = controller.IsPointingToRightDirection ? controller.AngleRight - 180 : controller.AngleLeft - 180;
        controller.Blockers.GetComponent<PlatformEffector2D>().rotationalOffset = controller.IsPointingToRightDirection ? controller.AngleRight - 180 : controller.AngleLeft - 180;
    }

    private void DisableNewColliders()
    {
        controller.OldSurfaceBlocker.SetActive(false);
        controller.SurfaceCollider.GetComponent<BoxCollider2D>().enabled = false;
        controller.Blockers.SetActive(false);

    }


}
