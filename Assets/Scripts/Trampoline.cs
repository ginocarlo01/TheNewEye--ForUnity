using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [SerializeField] float bounce = 20f;
    [SerializeField] private AudioSource trampolineSFX;
    private Animator animator;

    private void Start() { animator = GetComponent<Animator>(); }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            trampolineSFX.Play();
            animator.SetBool("collideToPlayer", true);
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * bounce, ForceMode2D.Impulse); //apply force to the player
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("collideToPlayer", false);
    }
}
