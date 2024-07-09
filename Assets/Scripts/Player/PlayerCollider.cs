using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{

    private PlayerMovement pm;

    [SerializeField] private bool floatingUp, floatingDown;


    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Up")
        {
            floatingUp = !floatingUp;
            pm.SetFloatingUp(floatingUp);
        }

        if (collision.gameObject.tag == "Down")
        {
            floatingDown = !floatingDown;
            pm.SetFloatingDown(floatingDown);
        }
    }
    
    
}
