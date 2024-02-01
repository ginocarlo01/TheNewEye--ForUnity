using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownCollider : MonoBehaviour
{
    [SerializeField] private bool canChangeTag;

    private Animator animator;

    public static Action<SFX> trampolineActionSFX;
    [SerializeField] protected SFX trampolineSFX;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    //collision with player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canChangeTag = true;
            trampolineActionSFX?.Invoke(trampolineSFX);
            animator.SetBool("collideToPlayer", true);
        }
    }

    //exit collision with player
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (canChangeTag)
            {
                //reverse UpDown
                if (gameObject.tag == "Up")
                {
                    gameObject.tag = "Down";
                }
                else
                {
                    if (gameObject.tag == "Down")
                    {
                        gameObject.tag = "Up";
                    }
                }
            }

            canChangeTag = false;

            animator.SetBool("collideToPlayer", false);

        }
    }
}
