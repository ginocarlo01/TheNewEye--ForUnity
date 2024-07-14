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
         controllerObj = controller.gameObject;
        controller.rb.bodyType = RigidbodyType2D.Dynamic;
        
    }

    public void OnUpdate()
    {
        if (!controller.Player || !controllerObj) return;

        // controllerObj.transform.position = Vector3.MoveTowards(controllerObj.transform.position, controller.Player.transform.position, Mathf.Abs(controller.Speed)*Time.deltaTime);
        MoveToPlayer();
        controller.IncreaseProjectileSpeed();

        float currentDistanceToPlayer = Vector3.Distance(controller.Player.transform.position, controllerObj.transform.position);

        if (currentDistanceToPlayer > controller.MaxDistanceToPlayer*2) { controller.Die(); }

        if (currentDistanceToPlayer < controller.MinDistanceToPlayer) { controller.BackToPlayer(); }
    }

    private void MoveToPlayer()
    {
        Vector3 pointToPlayer = (controller.Player.transform.position - controllerObj.transform.position).normalized;

        pointToPlayer *= Mathf.Abs(controller.FollowPlayerSpeed);

        controller.rb.velocity = new Vector2(pointToPlayer.x, pointToPlayer.y);

    }

    
}
