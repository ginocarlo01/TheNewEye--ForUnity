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
        JsonReadWriteSystem.INSTANCE.playerData.ChangeBoolAtIndex(JsonReadWriteSystem.INSTANCE.currentLvlIndex, true);
        Debug.Log("trocou o level completed");
        Debug.Log("novo level completed: " + JsonReadWriteSystem.INSTANCE.playerData.arrayOfLevels[JsonReadWriteSystem.INSTANCE.currentLvlIndex].levelCompleted);
        JsonReadWriteSystem.INSTANCE.Save();
        SceneManager.LoadScene(nextLevelName);
    }
    public void startConstantCheckPointFlag()
    {
        
    }
}
