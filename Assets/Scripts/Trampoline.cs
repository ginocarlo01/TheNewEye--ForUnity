using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float bounce = 20f;
    [SerializeField] protected Animator animator;

    [SerializeField] private SFX trampolineSFX;

    public static Action<SFX> trampolineActionSFX;

    private void Start() { if (!animator) animator = GetComponent<Animator>(); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            CollideWithPlayer(collision);
            TriggerAnim();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (animator) animator.SetBool("collideToPlayer", false);
    }

    public void CollideWithPlayer(Collision2D collision)
    {
        trampolineActionSFX?.Invoke(trampolineSFX);
        
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse); //apply force to the player
    }

    public void TriggerAnim()
    {
        if (animator) animator.SetBool("collideToPlayer", true);
    }
}
