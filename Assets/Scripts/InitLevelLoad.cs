using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitLevelLoad : MonoBehaviour
{
    [SerializeField] private bool canGo, alreadyGo;
    [SerializeField] private int levelIndex;

    [SerializeField] private Animator animator;

    [SerializeField] private AudioSource enterSFX;

    [SerializeField] private AnimationClip movingDoor, endDoor;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnterLevelUI.instance.SetLevelText("Level " + (levelIndex+1));
        canGo = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        EnterLevelUI.instance.SetLevelText("");
        canGo = false;
    }

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
        if (Input.GetButtonDown("Jump") && canGo && !alreadyGo)
        {
            PlayerMovement.instance.StopPlayerMovement();
            alreadyGo = true;
            enterSFX.Play();
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
        SceneManager.LoadScene("lvl" + levelIndex);
    }
}
