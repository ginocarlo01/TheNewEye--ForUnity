using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;

    private bool touchedCheckPoint = false;

    [SerializeField] private AnimationClip releaseCheckPointFlag, constantCheckPointFlag;

    [SerializeField] private SFX checkPointSFX;

    public static Action<SFX> checkPointActionSFX;

    public static Action<Vector3> saveCheckPointAction;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!touchedCheckPoint && collision.gameObject.tag == "Player")
        {
            touchedCheckPoint = true;
            animator.Play(releaseCheckPointFlag.name);
            checkPointActionSFX?.Invoke(checkPointSFX);
            saveCheckPointAction?.Invoke(transform.position);
        }
    }

    public void startConstantCheckPointFlag()
    {
        animator.Play(constantCheckPointFlag.name);
    }
}
