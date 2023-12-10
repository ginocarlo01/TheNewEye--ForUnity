using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private string firstLevelName;

    [SerializeField] private GameObject controlsTxt;

    private bool selectedButton;

    private void Start()
    {
        controlsTxt.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevelName);
    }

    public void ShowOptions()
    {
        selectedButton = !selectedButton;

        controlsTxt.SetActive(selectedButton);
        
    }
}
