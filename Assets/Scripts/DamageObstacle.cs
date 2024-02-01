using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObstacle : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerLife playerLife = collision.gameObject.GetComponent<PlayerLife>();
            if (playerLife != null) { playerLife.Die(); }
        }
    }
}
