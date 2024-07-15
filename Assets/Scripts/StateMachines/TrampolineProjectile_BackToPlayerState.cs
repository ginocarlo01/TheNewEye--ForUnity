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

    public void OnBeginState()
    {
         controllerObj = controller.gameObject;
        controller.GetComponent<Animator>().SetTrigger("collideToPlayer");
        controller.rb.bodyType = RigidbodyType2D.Dynamic;
        
    }

    public void OnUpdate()
    {
        if (!controller.Player || !controllerObj) return;

        // controllerObj.transform.position = Vector3.MoveTowards(controllerObj.transform.position, controller.Player.transform.position, Mathf.Abs(controller.Speed)*Time.deltaTime);
        MoveToPlayer();
        controller.IncreaseProjectileSpeed();

        float currentDistanceToPlayer = Vector3.Distance(controller.Player.transform.position, controllerObj.transform.position);

        if (currentDistanceToPlayer > controller.MaxDistanceToPlayer*2) { controller.ReceiveProjectile(); }

        if (currentDistanceToPlayer < controller.MinDistanceToPlayer) { controller.ReceiveProjectile(); }
    }

    private void MoveToPlayer()
    {
        Vector3 pointToPlayer = (controller.Player.transform.position - controllerObj.transform.position).normalized;

        pointToPlayer.z = 0;

        controllerObj.transform.rotation = Quaternion.LookRotation(Vector3.forward, pointToPlayer);

        pointToPlayer *= Mathf.Abs(controller.FollowPlayerSpeed);

        controller.rb.velocity = new Vector2(pointToPlayer.x, pointToPlayer.y);

    }

    public IThrowObjectState ChangeState()
    {
        return null;
    }


}
