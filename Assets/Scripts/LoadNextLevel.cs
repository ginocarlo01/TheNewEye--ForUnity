using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    [SerializeField] private AudioSource checkPointSFX;

    [SerializeField] private string nextLevelName;

    [SerializeField] private float waitBeforeNextLevel = 2f;

    private bool touchedCheckPoint = false;

    private Animator animator;

    [SerializeField] private AnimationClip releaseCheckPointFlag;

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
            checkPointSFX.Play();
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
