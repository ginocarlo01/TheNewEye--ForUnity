/*
 * Written by: ginocarlo01
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject optionsUIAnim;
    private bool optionsWasPressed = false;

    private void Start()
    {
        InitUI();
    }

    public void HandleStartButtonOnClickEvent(string _scene)
    {
        SceneManager.LoadScene(_scene);
    }

    public void HandleOptionsButtonOnClickEvent()
    {
        if (optionsWasPressed)
        {
            optionsWasPressed = false;
            optionsUIAnim.SetActive(optionsWasPressed);
        }
        else
        {
            optionsWasPressed = true;
            optionsUIAnim.SetActive(optionsWasPressed);
        }
    }

    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }

    private void InitUI()
    {
        optionsUIAnim = GameObject.FindGameObjectWithTag("OptionsUI");
        optionsUIAnim.SetActive(false);
        Cursor.visible = true;
        // Libera o cursor para se movimentar livremente
        Cursor.lockState = CursorLockMode.None;
    }
}
