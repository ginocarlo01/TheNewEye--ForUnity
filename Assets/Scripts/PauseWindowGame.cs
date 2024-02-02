/*
 * Written by: ginocarlo01
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EasyTransition;
public class PauseWindowGame : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsUI;

    private bool optionsWasPressed = false;

    [SerializeField]
    private Signal pauseSignal;

    public TransitionSettings transition;
    public float startTransitionDelay;

    private void Start() {
        optionsUI.SetActive(optionsWasPressed);
    }


    public void HandleStartButtonOnClickEvent()
    {
        pauseSignal.Raise();
    }

    public void HandleOptionsButtonOnClickEvent()
    {
        if (optionsWasPressed)
        {
            optionsWasPressed = false;
            optionsUI.SetActive(optionsWasPressed);
        }
        else
        {
            optionsWasPressed = true;
            optionsUI.SetActive(optionsWasPressed);
        }
    }

    public void HandleQuitButtonOnClickEvent(string _scene)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(_scene);
    }

    

    
}
