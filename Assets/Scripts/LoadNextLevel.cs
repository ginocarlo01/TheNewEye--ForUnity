using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] private string nextLevelName;

    [SerializeField] private float waitBeforeNextLevel = 2f;

    private bool touchedCheckPoint = false;

    private Animator animator;

    [SerializeField] private AnimationClip releaseCheckPointFlag;

    [SerializeField] private SFX finishLevelSFX;

    public static Action<SFX> finishLevelActionSFX;

    public static Action finishLevelAction;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (!touchedCheckPoint && collision.gameObject.tag == "Player")
        {
            animator.Play(releaseCheckPointFlag.name);
            touchedCheckPoint = true;
            finishLevelActionSFX?.Invoke(finishLevelSFX);

            Invoke("CompleteLevel", waitBeforeNextLevel);
        }
         
    }  

    private void CompleteLevel()
    {
        finishLevelAction?.Invoke();
        SceneManager.LoadScene(nextLevelName);
    }
    

}
