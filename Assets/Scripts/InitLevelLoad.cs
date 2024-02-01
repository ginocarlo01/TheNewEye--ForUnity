using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;
using System;
public class InitLevelLoad : MonoBehaviour
{
    private bool playerInside, playerGone;
    
    [SerializeField] private int levelIndex;

    [Header("Animation Settings")]
    private Animator animator;
    [SerializeField] private AnimationClip movingDoor, endDoor;

    [Header("Transition Settings")]
    public TransitionSettings transition;
    public float startDelay;

    //Actions
    public static Action<int> playerEnteredDoor;
    public static Action playerEnteredLevel;
    public static Action<SFX> playerEnterActionSFX;
    [SerializeField] private SFX playerEnterSFX;


    private void Start()
    {
        
        if(levelIndex != 0)
        {
            //Debug.Log("O level " + (levelIndex - 1).ToString() + "foi completed: " + CheckIfLevelIsCompleted(levelIndex - 1));
            this.gameObject.SetActive(CheckIfLevelIsCompleted(levelIndex - 1));  
        }

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump") && playerInside && !playerGone)
        {
            playerEnteredLevel?.Invoke();
            playerGone = true;
            playerEnterActionSFX?.Invoke(playerEnterSFX);
            animator.Play(movingDoor.name);
        }
    }

    private bool CheckIfLevelIsCompleted(int levelIndex)
    {
        return JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[levelIndex].levelCompleted;
    }

    private void MoveToLevel()
    {
        animator.Play(endDoor.name);
        TransitionManager.Instance().Transition("lvl" + levelIndex, transition, startDelay);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerEnteredDoor?.Invoke(levelIndex + 1);
        playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerEnteredDoor?.Invoke(-1);
        playerInside = false;
    }

}
