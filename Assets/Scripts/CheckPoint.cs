using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator animator;

    private bool touchedCheckPoint = false;

    [SerializeField] private AnimationClip releaseCheckPointFlag, constantCheckPointFlag;

    [SerializeField] private AudioSource checkPointSFX;

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
            checkPointSFX.Play();
            JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].lastLocation = transform.position;
            Debug.Log("Acabou de gravar o checkpoint!");
        }
    }

    public void startConstantCheckPointFlag()
    {
        animator.Play(constantCheckPointFlag.name);
    }
}
